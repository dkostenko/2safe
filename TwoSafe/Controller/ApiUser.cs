using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Drawing;
using System.Globalization;
using System.Web.Script.Serialization;
using Microsoft.Win32;

namespace TwoSafe.Controller
{

    /// <summary>
    /// API 2safe.ru
    /// </summary>
    public static class ApiTwoSafe
    {
        const string baseUrl = "https://api.2safe.com/?cmd=";

        /// <summary>
        /// Проверяет email на доступность
        /// </summary>
        /// <param name="email">email</param>
        public static Dictionary<string, dynamic> checkEmail(string email)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "chk_mail" + "&email=" + email));
        }

        /// <summary>
        /// Проверяет аккаунт на доступность
        /// </summary>
        /// <param name="login">Аккаунт, который необходимо проверить</param>
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
        /// <param name="login">Новый аккаунт</param>
        /// <param name="password">Пароль</param>
        /// <param name="email">Email, на который будет зарегистрирован аккаунт</param>
        /// <param name="captcha">Капча, введенная с картинки</param>
        /// <param name="id">ID капчи</param>
        public static Dictionary<string, dynamic> addLogin(string login, string password, string email, string captcha, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "add_login" + "&login=" + login + "&password=" + password + "&email=" + email + "&captcha=" + captcha + "&id=" + id));
        }

        
        /// <summary>
        /// Аутентификация
        /// </summary>
        /// <param name="login">Акаунт пользователя</param>
        /// <param name="password">Пароль от аккаунта</param>
        /// <param name="captcha_id">ID капчи</param>
        /// <param name="captcha">Сама капча</param>
        public static Dictionary<string, dynamic> auth(string login, string password, string captcha_id, string captcha)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "auth" + "&login=" + login + "&password=" + password + "&captcha_id=" + captcha_id + "&captcha=" + captcha));
        }

        /// <summary>
        /// Аутентификация
        /// </summary>
        /// <param name="login">Акаунт пользователя</param>
        /// <param name="password">Пароль от аккаунта</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> auth(string login, string password, Dictionary<string, string> optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "auth" + "&login=" + login + "&password=" + password + toQueryString(optional)));
        }

        /// <summary>
        /// Удаление пользователя
        /// </summary>
        /// <param name="login">Аккаунт пользователя</param>
        /// <param name="password">Пароль от аккаунта</param>
        public static Dictionary<string, dynamic> removeLogin(string login, string password)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "remove_login" + "&login=" + login + "&password=" + password));
        }

        /// <summary>
        /// Удаление сессии
        /// </summary>
        /// <param name="token">Токен</param>
        public static Dictionary<string, dynamic> logout(string token)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "auth" + "&token=" + token));
        }

        /// <summary>
        /// Просмотр квоты
        /// </summary>
        /// <param name="token">Токен</param>
        public static Dictionary<string, dynamic> getDiskQuota(string token)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_disk_quota" + "&token=" + token));
        }

        /// <summary>
        /// Получение карточки юзера
        /// </summary>
        /// <param name="token">Токен</param>
        public static Dictionary<string, dynamic> getPersonalData()
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_personal_data" + "&token=" + Properties.Settings.Default.Token));
        }

        /// <summary>
        /// Изменение карточки юзера
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="personal">Новые данные пользователя</param>
        /// <param name="props">Произвольные данные</param>
        /// <param name="password">Пароль</param>
        public static Dictionary<string, dynamic> setPersonalData(string token, Dictionary<string, string> personal, Dictionary<string, string> props, string password)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            string optional = toJson(props);
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "set_personal_data" + "&token=" + token + "&personal=" + toJson(personal) + "&password=" + password));
        }

        /// <summary>
        /// Смена пароля
        /// </summary>
        /// <param name="login">Аккаунт</param>
        /// <param name="password">Пароль</param>
        /// <param name="newPassword">Новый пароль</param>
        public static Dictionary<string, dynamic> changePassword(string login, string password, string newPassword)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "change_password" + "&login=" + login + "&password=" + password + "&new_password=" + newPassword));
        }

        /// <summary>
        /// Активация промо кода
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="code">Промо код</param>
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
        public static void getFile(string id, string token, Dictionary<string, string> optional, string localPath)
        {
            WebClient wc = new WebClient();
            wc.DownloadFileAsync(new Uri("https://api.2safe.com/?cmd=get_file&id=" + id + "&token=" + token + toQueryString(optional)), localPath);
        }

        /// <summary>
        /// Загружает 1 файл и необходимые строковые данные на сервер методом POST (multipart/form-data)
        /// </summary>
        /// <param name="postData">Данные: необходимые для передачи файла (id папки, токен и другие)</param>
        /// <param name="fileName">Полный путь до файла</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> putFile(string dir_id,  string fileName, Dictionary<string, string> postData)
        {
            FileStream fileData = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create("https://api.2safe.com/?cmd=put_file");

            webrequest.Method = "POST";

            string ctype;

            string fileContentType = tryGetContentType(fileName, out ctype) ? ctype : "application/octet-stream";

            string boundary = "----------" + DateTime.Now.Ticks.ToString("x", CultureInfo.InvariantCulture);

            webrequest.ContentType = "multipart/form-data; boundary=" + boundary;

            StringBuilder sbHeader = new StringBuilder();

            // Добавление строковых параметров
            sbHeader.AppendFormat("--{0}\r\n", boundary);
            sbHeader.AppendFormat("Content-Disposition: form-data; name=\"token\";\r\n\r\n" + Properties.Settings.Default.Token + "\r\n");

            sbHeader.AppendFormat("--{0}\r\n", boundary);
            sbHeader.AppendFormat("Content-Disposition: form-data; name=\"dir_id\";\r\n\r\n" + dir_id + "\r\n");

            if (postData != null)
            {
                foreach (var one in postData)
                {
                    sbHeader.AppendFormat("--{0}\r\n", boundary);
                    sbHeader.AppendFormat("Content-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}\r\n", one.Key, one.Value);
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


                // Запись файла
                byte[] buffer = new Byte[checked((uint)Math.Min(4096, (int)fileData.Length))];
                int bytesRead;
                while ((bytesRead = fileData.Read(buffer, 0, buffer.Length)) != 0)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                }

                // Конец записи
                requestStream.Write(footer, 0, footer.Length);
                fileData.Close();

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
        /// <param name="token">Токен</param>
        /// <param name="id">ID файла</param>
        /// <param name="dir_id">ID папки, куда копируется файл</param>
        /// <param name="optional">Опциональные параметры</param>
        public static Dictionary<string, dynamic> copyFile(string id, string dirId, string token, Dictionary<string, string> optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "copy_file&token=" + token + "&id=" + id + "&dir_id=" + dirId + toQueryString(optional)));
        }

        /// <summary>
        /// Перемещение файлов
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID файла</param>
        /// <param name="dir_id">ID папки, куда перемещается файл</param>
        /// <param name="optional">Опциональные параметры</param>
        public static Dictionary<string, dynamic> moveFile(string id, string dirId, string token, Dictionary<string, string> optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "move_file&token=" + token + "&id=" + id + "&dir_id=" + dirId + toQueryString(optional)));
        }

        /// <summary>
        /// Удаление файлов
        /// </summary>
        /// <param name="id">ID файла</param>
        /// <param name="token">Токен</param>
        /// <param name="removeNow">Удалить, минуя корзину (аналог Shift+Del)</param>
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
        /// <param name="dirId">ID родительской папки</param>
        /// <param name="dirName">Имя папки</param>
        /// <param name="token">токен</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> makeDir(string dirId, string dirName, Dictionary<string, string> optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, dynamic> result = jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "make_dir&token=" + Properties.Settings.Default.Token + "&dir_id=" + dirId + "&dir_name=" + dirName + toQueryString(optional)));
            return result;
        }

        /// <summary>
        /// Копирование директории
        /// </summary>
        /// <param name="id">ID папки, которую необходимо скопировать</param>
        /// <param name="dirId">ID папки: в которую будет каопироваться</param>
        /// <param name="token">токен</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> copyDir(string dirId, string id, string token, Dictionary<string, string> optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "copy_dir&token=" + token + "&dir_id=" + dirId + "&id=" + id + toQueryString(optional)));
        }

        /// <summary>
        /// Перемещение директории
        /// </summary>
        /// <param name="id">ID папки, которую необходимо переместить</param>
        /// <param name="dirId">ID папки: в которую будет перемещаться</param>
        /// <param name="token">токен</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> moveDir(string dirId, string id, string token, Dictionary<string, string> optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "move_dir&token=" + token + "&dir_id=" + dirId + "&id=" + id + toQueryString(optional)));
        }

        /// <summary>
        /// Удаление директории
        /// </summary>
        /// <param name="dirId">ID корневой папки</param>
        /// <param name="optional">Опциональные параметры</param>
        /// <param name="removeNow">Удалить минуя корзину (аналог Shift+Del)</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> removeDir(string dirId, Dictionary<string, string> optional, bool removeNow)
        {
            string optionals = toQueryString(optional);
            if (removeNow)
            {
                optionals = "&remove_now=true";
            }

            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, dynamic> json = jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "remove_dir&token=" + Properties.Settings.Default.Token + "&dir_id=" + dirId + optionals));
            if (!json.ContainsKey("error_code"))
            {
                Helpers.ApplicationHelper.SetCurrentTimeToSettings();
            }
            return json;
        }

        /// <summary>
        /// Просмотр директории
        /// </summary>
        /// <param name="dirId">ID каталога, при пустом значении выдает список файлов и папко корневого каталога</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> listDir(string dirId)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "list_dir&token=" + Properties.Settings.Default.Token + "&dir_id=" + dirId));
        }

        /// <summary>
        /// Получение свойств объекта
        /// </summary>
        /// <param name="id">ID объекта</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> getProps(string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_props&token=" + Properties.Settings.Default.Token + "&id=" + id));
        }

        /// <summary>
        /// Получение родительского дерева каталогов
        /// </summary>
        /// <param name="dir_id">ID папки</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> getTreeParent(string dir_id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_tree_parent&token=" + Properties.Settings.Default.Token + "&dir_id=" + dir_id));
        }

        /// <summary>
        /// Просмотр действий пользователя
        /// </summary>
        /// <param name="after">timestamp в формате наносекунд. (без этого параметра выводятся последние 300 событий)</param>
        /// <returns></returns>
        public static Dictionary<string, dynamic> getEvents(string after)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            Dictionary<string, dynamic> json = jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_events&token=" + Properties.Settings.Default.Token + "&after=" + after));
            if (json != null && !json.ContainsKey("error_code"))
            {
                Helpers.ApplicationHelper.SetCurrentTimeToSettings();
            }
            return json;
        }



        /// <summary>
        /// Блокировка объекта
        /// </summary>
        /// <param name="token">токен</param>
        /// <param name="id">ID объекта</param>
        /// <param name="optional">Опциональные параметры</param>
        public static Dictionary<string, dynamic> lockObject(string token, string id, Dictionary<string, string> optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "lock_object" + "&token=" + token + "&id=" + id + toQueryString(optional)));
        }

        /// <summary>
        /// Разблокировка объекта
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="lockToken">Токен блокированного объекта</param>
        /// <param name="optional">Опциональные параметры</param>
        public static Dictionary<string, dynamic> unlockObject(string token, string lockToken)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "unlock_object" + "&token=" + token + "&lock_token=" + lockToken));
        }

        /// <summary>
        /// Получение списка всех блокировок
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID Объекта</param>
        public static Dictionary<string, dynamic> listObjectLocks(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "list_object_locks" + "&token=" + token + "&id=" + id));
        }

        /// <summary>
        /// Обновление таймаута блокировки
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID Объекта</param>
        /// <param name="timeout">Новый таймаут</param>
        public static Dictionary<string, dynamic> refreshLockTimeout(string token, string lockToken, string timeout)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "refresh_lock_timeout" + "&token=" + token + "&lockToken=" + lockToken + "&timeout=" + timeout));
        }

        /// <summary>
        /// Публичность объекта
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID объекта</param>
        public static Dictionary<string, dynamic> publicObject(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "public_object" + "&token=" + token + "&id=" + id));
        }

        /// <summary>
        /// Отменить публичность объекта
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID объекта</param>
        public static Dictionary<string, dynamic> unpublicObject(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "inpublic_object" + "&token=" + token + "&id=" + id));
        }


        /// <summary>
        /// Расшаривание объекта
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="login">Акаунт пользователя, для которого необходимо расшарить файл</param>
        /// <param name="id">ID объекта</param>
        /// <param name="optional">Опциональные параметры</param>
        public static Dictionary<string, dynamic> shareObject(string token, string login, string id, Dictionary<string, string> optional)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "share_object" + "&token=" + token + "&login=" + login + "&id=" + id + toQueryString(optional)));
        }

        /// <summary>
        /// Отменить расшаривание объекта
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="login">Акаунт пользователя, для которого был расшарен файл</param>
        /// <param name="id">ID объекта</param>
        public static Dictionary<string, dynamic> unshareObject(string token, string login, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "unshare_object" + "&token=" + token + "&login=" + login + "&id=" + id));
        }

        /// <summary>
        /// Список шар объекта
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID объекта</param>
        public static Dictionary<string, dynamic> listShares(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "list_shares" + "&token=" + token + "&id=" + id));
        }


        /// <summary>
        /// Получение списка версий файла
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID файла</param>
        public static Dictionary<string, dynamic> listVersions(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "list_versions" + "&token=" + token + "&id=" + id));
        }

        /// <summary>
        /// Получение текущей версии файла
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID файла</param>
        public static Dictionary<string, dynamic> getCurrentVersion(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_current_version" + "&token=" + token + "&id=" + id));
        }

        /// <summary>
        /// Установка текущей версии файла
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID файла</param>
        /// <param name="v_id">ID версии</param>
        public static Dictionary<string, dynamic> setCurrentVersion(string token, string id, string v_id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "set_current_version" + "&token=" + token + "&id=" + id + "&v_id=" + v_id));
        }

        /// <summary>
        /// Удаление версии файла
        /// </summary>
        /// <param name="token">Токен</param>
        /// <param name="id">ID версии</param>
        public static Dictionary<string, dynamic> removeVersion(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "remove_version" + "&token=" + token + "&id=" + id));
        }

        /// <summary>
        /// Установка флага версионности для объекта
        /// </summary>
        /// <remark>
        /// Если флагом помечается каталог, то все файлы в этом и во вложенных каталогах будут версионными
        /// </remark>
        /// <param name="token">Токен</param>
        /// <param name="id">ID файла или каталога</param>
        public static Dictionary<string, dynamic> setVersioned(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "set_versioned" + "&token=" + token + "&id=" + id));
        }

        /// <summary>
        /// Установка флага версионности для объекта
        /// </summary>
        /// <remark>
        /// Команда применима только к тем обхектам, для которых была выполнена команда set_versioned. Если флаг снимается с каталога, то все файлы в этом и во вложенных каталогах больше не будут версионными, за исключением случая, если на вложенный файл не проставлялся этот флаг.
        /// </remark>
        /// <param name="token">Токен</param>
        /// <param name="id">ID файла или каталога</param>
        public static Dictionary<string, dynamic> unsetVersioned(string token, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "unset_versioned" + "&token=" + token + "&id=" + id));
        }


        /// <summary>
        /// Переводит список параметров в JSON для GET-запроса
        /// </summary>
        /// <param name="parameters">Список параметров</param>
        /// <returns>
        /// Возвращает список параметров как JSON для GET-запроса
        /// </returns>
        private static string toJson(Dictionary<string, string> parameters)
        {
            string result = "";

            if (parameters != null)
            {
                result += "{";
                foreach (var parameter in parameters)
                {
                    result += String.Concat("\"", parameter.Key, "\":\"", parameter.Value, "\",");
                }
                result += "}";
            }

            return result;
        }

        /// <summary>
        /// Переводит список параметров в строку для GET-запроса
        /// </summary>
        /// <param name="parameters">Список параметров</param>
        /// <returns>
        /// Возвращает список параметров как строку для GET-запроса
        /// </returns>
        private static string toQueryString(Dictionary<string, string> parameters)
        {
            string result = "";

            if (parameters != null)
            {
                foreach (var parameter in parameters)
                {
                    result += String.Concat("&", parameter.Key, "=", parameter.Value);
                }
            }

            return result;
        } 

        /// <summary>
        /// Пытается посмотреть тип файла (например: картинка, текстовый)
        /// </summary>
        /// <param name="fileName">Полный путь до файла</param>
        /// <param name="contentType">Тип файла</param>
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
        /// <param name="url">Ссылка, на которую необходимо отправить GET-запрос</param>
        /// <returns>
        /// Возвращает ответ сервера в виде строки с JSON
        /// </returns>
        private static string sendGET(string url)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            string output = "";
            try
            {
                HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
                Stream stream = resp.GetResponseStream();
                StreamReader sr = new StreamReader(stream);
                output = sr.ReadToEnd();
                sr.Close();
            }
            catch (WebException exception)
            {
                using (var reader = new StreamReader(exception.Response.GetResponseStream()))
                {
                    output = reader.ReadToEnd();
                }
            }
            catch (Exception ex)
            {
                output = "";
            }
            return output;
        }

    }
}
