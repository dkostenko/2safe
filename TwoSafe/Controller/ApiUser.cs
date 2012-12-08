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

        public static dynamic checkEmail(NameValueCollection data)
        {
            NameValueCollection postData = new NameValueCollection();

            JavaScriptSerializer json = new JavaScriptSerializer();
            var dict = json.Deserialize<dynamic>("asd");
            var dictw = json.Deserialize<dynamic>("asd");

            return dict;
        }

        public static Dictionary<string, string> checkLogin(string login)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, string>>(sendGET(baseUrl + "chk_login" + "&login=" + login));
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

        public static Dictionary<string, dynamic> addLogin(string login, string password, string email, string captcha, string id)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "add_login" + "&login=" + login + "&password=" + password + "&email=" + email + "&captcha=" + captcha + "&id=" + id));
        }

        public static Dictionary<string, dynamic> auth(string login, string password)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "auth" + "&login=" + login + "&password=" + password));
        }

        public static dynamic removeLogin(NameValueCollection data)
        {
            return null;
        }

        public static dynamic logout(NameValueCollection data)
        {
            return null;
        }

        public static dynamic getDiskQuota(NameValueCollection data)
        {
            return null;
        }

        public static Dictionary<string, dynamic> getPersonalData(string token)
        {
            JavaScriptSerializer jss = new JavaScriptSerializer();
            return jss.Deserialize<Dictionary<string, dynamic>>(sendGET(baseUrl + "get_personal_data" + "&token=" + token));
        }

        public static dynamic setPersonalData(NameValueCollection data)
        {
            return null;
        }

        public static dynamic changePassword(NameValueCollection data)
        {
            return null;
        }

        public static dynamic activatePromoCode(NameValueCollection data)
        {
            return null;
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
                            //TODO: use JSON.net to parse this string and look at the error message
                        }
                    }
                }
            }
            return output;
        }
    }
}
