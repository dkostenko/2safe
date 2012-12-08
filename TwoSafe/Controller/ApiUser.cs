using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Drawing;
using System.Globalization;
using System.Collections.Specialized;
using System.Web.Script.Serialization;
using Microsoft.Win32;

namespace TwoSafe.Controller
{
    public static class ApiTwoSafe
    {
        const string baseUrl = "https://api.2safe.com/?cmd=";

        /// <summary>
        /// Проверяет email на доступность
        /// </summary>
        public static Dictionary<string, dynamic> checkEmail(string email)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "chk_mail" + "&email=" + email));
        }

        /// <summary>
        /// Проверяет аккаунт на доступность
        /// </summary>
        public static Dictionary<string, dynamic> checkLogin(string login)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "chk_login" + "&login=" + login));
        }

        /// <summary>
        /// Получить капчу
        /// </summary>
        /// <returns>
        /// Возвращает Object[], где [0] - Bitmap captcha, [1] - string CaptchaID
        /// </returns>
        public static Object[] getCaptcha()
        {
            Object[] captcha = new Object[2];
            string temp;

            WebRequest req = WebRequest.Create("https://api.2safe.com/?cmd=get_captcha");

            try
            {
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                captcha[0] = new Bitmap(stream);
                temp = resp.Headers.GetValues(4)[0];
                temp = temp.Substring(13).Split(';')[0];
                captcha[1] = temp;

                stream.Close();
            }
            catch
            {
            }
            return captcha;
        }

        /// <summary>
        /// Регистрирует пользователя
        /// </summary>
        public static Dictionary<string, dynamic> addLogin(string login, string password, string email, string captcha, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "add_login" + "&login=" + login + "&password=" + password + "&email=" + email + "&captcha=" + captcha + "&id=" + id));
        }

        /// <summary>
        /// Аутентификация
        /// </summary>
        public static Dictionary<string, dynamic> auth(string login, string password)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "auth" + "&login=" + login + "&password=" + password));
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        public static Dictionary<string, dynamic> removeLogin(string login, string password)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "remove_login" + "&login=" + login + "&password=" + password));
        }

        /// <summary>
        /// Удаление сессии
        /// </summary>
        public static Dictionary<string, dynamic> logout(string token)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "auth" + "&token=" + token));
        }

        /// <summary>
        /// Просмотр квоты
        /// </summary>
        public static Dictionary<string, dynamic> getDiskQuota(string token)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_disk_quota" + "&token=" + token));
        }

        /// <summary>
        /// Получение карточки юзера
        /// </summary>
        public static Dictionary<string, dynamic> getPersonalData(string token)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_personal_data" + "&token=" + token));
        }

        /// <summary>
        /// Изменение карточки юзера
        /// </summary>
        private static Dictionary<string, dynamic> setPersonalData(NameValueCollection data)
        {
            return null;
        }

        /// <summary>
        /// Смена пароля
        /// </summary>
        public static Dictionary<string, dynamic> changePassword(string login, string password, string newPassword)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "change_password" + "&login=" + login + "&password=" + password + "&new_password=" + newPassword));
        }

        /// <summary>
        /// Активация промо кода
        /// </summary>
        public static Dictionary<string, dynamic> activatePromoCode(string token, string code)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "activate_promo_code" + "&token=" + token + "&code=" + code));
        }

        /// <summary>
        /// Получение файла
        /// </summary>
        /// <param name="id">id файла</param>
        /// <param name="token">Токен</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <param name="localPath">Путь до папки: где будет сохранен файл (с указанием нового имени файла)</param>
        /// <returns></returns>
        public static void getFile(string id, string token, NameValueCollection optional, string localPath)
        {
            WebClient wc = new WebClient();
            wc.DownloadFileAsync(new Uri("https://api.2safe.com/?cmd=get_file&id=" + id + "&token=" + token), localPath);
        }

        /// <summary>
        /// Загружает 1 файл и необходимые строковые данные на сервер методом POST (multipart/form-data)
        /// </summary>
        /// <param name="postData">Данные: необходимые для передачи файла (id папки, токен и другие)</param>
        /// <param name="fileName">Полный путь до файла</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> putFile(NameValueCollection postData, string fileName)
        {
            FileStream fileData = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://api.2safe.com/?cmd=put_file");

            webrequest.Method = "POST";

            string ctype;

            string fileContentType = tryGetContentType(fileName, out ctype) ? ctype : "application/octet-stream";

            string boundary = "----------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);

            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;

            StringBuilder sbHeader = new StringBuilder();

            // add form fields, if any
            if (postData != null)
            {
                foreach (string key in postData.AllKeys)
                {
                    string[] values = postData.GetValues(key);
                    if (values != null)
                        foreach (string value in values)
                        {
                            sbHeader.AppendFormat("--{0}\r\n", boundary);
                            sbHeader.AppendFormat("Content-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}\r\n", key,
                                                  value);
                        }
                }
            }


            if (fileData != null)
            {
                sbHeader.AppendFormat("--{0}\r\n", boundary);
                sbHeader.AppendFormat("Content-Disposition: form-data; name=\"file\"; {0}\r\n", string.IsNullOrEmpty(fileName) ?
                                      "" : string.Format(CultureInfo.InvariantCulture, "filename=\"{0}\";", Path.GetFileName(fileName)));

                sbHeader.AppendFormat("Content-Type: {0}\r\n\r\n", fileContentType);
            }

            byte[] header = Encoding.UTF8.GetBytes(sbHeader.ToString());
            byte[] footer = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
            long contentLength = header.Length + (fileData != null ? fileData.Length : 0) + footer.Length;

            webrequest.ContentLength = contentLength;

            using (Stream requestStream = webrequest.GetRequestStream())
            {
                requestStream.Write(header, 0, header.Length);


                if (fileData != null)
                {
                    // write the file data, if any
                    byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileData.Length))];
                    int bytesRead;
                    while ((bytesRead = fileData.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }
                }

                // write footer
                requestStream.Write(footer, 0, footer.Length);

                string output = "";
                try
                {
                    WebResponse resp = webrequest.GetResponse();
                    Stream stream = resp.GetResponseStream();
                    StreamReader sr = new StreamReader(stream);
                    output = sr.ReadToEnd();
                    sr.Close();
                }
                catch (WebException wex)
                {
                    if (wex.Response != null)
                    {
                        using (var errorResponse = (HttpWebResponse)wex.Response)
                        {
                            using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                            {
                                output = reader.ReadToEnd();
                                //TODO: use JSON.net to parse this string and look at the error message
                            }
                        }
                    }
                }

                JavaScriptSerializer jss = new JavaScriptSerializer();
                return jss.Deserialize<Dictionary<string, dynamic>>(output);
            }
        }

        /// <summary>
        /// Копирование файлов
        /// </summary>
        public static Dictionary<string, dynamic> copyFile(string id, string dirId, string token, NameValueCollection optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "copy_file&token=" + token + "&id=" + id + "&dir_id=" + dirId));
        }

        /// <summary>
        /// Перемещение файлов
        /// </summary>
        public static Dictionary<string, dynamic> moveFile(string id, string dirId, string token, NameValueCollection optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "move_file&token=" + token + "&id=" + id + "&dir_id=" + dirId));
        }

        /// <summary>
        /// Удаление файлов
        /// </summary>
        /// <param name="id">id файла</param>
        /// <param name="token">токен</param>
        /// <param name="removeNow">удалить минуя корзину (аналог Shift+Del)</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> removeFile(string id, string token, bool removeNow)
        {
            string optional = "";
            if (removeNow)
            {
                optional = "&remove_now=true";
            }
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "remove_file&token=" + token + "&id=" + id + optional));
        }

        /// <summary>
        /// Создание директории
        /// </summary>
        /// <param name="dirId">id корневой папки</param>
        /// <param name="dirName">Имя папки</param>
        /// <param name="token">токен</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> makeDir(string dirId, string dirName, string token, NameValueCollection optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "make_dir&token=" + token + "&dir_id=" + dirId + "&dir_name=" + dirName));
        }

        /// <summary>
        /// Копирование директории
        /// </summary>
        /// <param name="id">id папки, которую необходимо скопировать</param>
        /// <param name="dirId">id папки: в которую будет каопироваться</param>
        /// <param name="token">токен</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> copyDir(string dirId, string id, string token, NameValueCollection optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "copy_dir&token=" + token + "&dir_id=" + dirId + "&id=" + id));
        }

        /// <summary>
        /// Перемещение директории
        /// </summary>
        /// <param name="id">id папки, которую необходимо переместить</param>
        /// <param name="dirId">id папки: в которую будет перемещаться</param>
        /// <param name="token">токен</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> moveDir(string dirId, string id, string token, NameValueCollection optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "move_dir&token=" + token + "&dir_id=" + dirId + "&id=" + id));
        }

        /// <summary>
        /// Удаление директории
        /// </summary>
        /// <param name="dirId">id корневой папки</param>
        /// <param name="token">токен</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <param name="removeNow">удалить минуя корзину (аналог Shift+Del)</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> removeDir(string dirId, string token, NameValueCollection optional, bool removeNow)
        {
            string optionals = "";
            if (removeNow)
            {
                optionals = "&remove_now=true";
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "remove_dir&token=" + token + "&dir_id=" + dirId + optionals));
        }

        /// <summary>
        /// Просмотр директории
        /// </summary>
        /// <param name="dirId">id каталога, при пустом значении выдает список файлов и папко корневого каталога</param>
        /// <param name="token">токен</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> listDir(string dirId, string token)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "List_dir&token=" + token + "&dir_id=" + dirId));
        }





        /// <summary>
        /// Пытается посмотреть тип файла (например: картинка, текстовый)
        /// </summary>
        /// <param name="fileName">Полный путь до файла</param>
        /// <param name="contentType">Тип файла</param>
        /// <returns></returns>
        private static bool tryGetContentType(string fileName, out string contentType)
        {
            try
            {
                RegistryKey key = Registry.ClassesRoot.OpenSubKey(@"MIME\Database\Content Type");

                if (key != null)
                {
                    foreach (string keyName in key.GetSubKeyNames())
                    {
                        RegistryKey subKey = key.OpenSubKey(keyName);
                        if (subKey != null)
                        {
                            string subKeyValue = (string)subKey.GetValue("Extension");

                            if (!string.IsNullOrEmpty(subKeyValue))
                            {
                                if (string.Compare(Path.GetExtension(fileName).ToUpperInvariant(),
                                                   subKeyValue.ToUpperInvariant(), StringComparison.OrdinalIgnoreCase) ==
                                    0)
                                {
                                    contentType = keyName;
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            // ReSharper disable EmptyGeneralCatchClause
            catch
            {
                // fail silently
                // TODO: rethrow registry access denied errors
            }
            // ReSharper restore EmptyGeneralCatchClause
            contentType = "";
            return false;
        }

        /// <summary>
        /// Отправляет GET-запрос на указанный адрес
        /// </summary>
        private static string sendGET(string url)
        {
            WebRequest req = WebRequest.Create(url);
            string output = "";
            try
            {
                WebResponse resp = req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                output = sr.ReadToEnd();
                sr.Close();
            }
            catch (WebException wex)
            {
                if (wex.Response != null)
                {
                    using (var errorResponse = (HttpWebResponse)wex.Response)
                    {
                        using (var reader = new StreamReader(errorResponse.GetResponseStream()))
                        {
                            output = reader.ReadToEnd();
                            //TODO: Обработать ошибку
                        }
                    }
                }
            }
            return output;
        }
    }
}
