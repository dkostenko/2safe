using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Drawing;
using Newtonsoft.Json;
using System.Collections.Specialized;

namespace TwoSafe.Controller
{
    public static class Connection
    {
        private static string baseUrl = "https://api.2safe.com/?cmd=";

        /// <summary>
        /// Отправляет запросы GET и POST
        /// </summary>
        /// <param name="type">
        /// Тип запроса. POST или GET
        /// </param>
        /// <param name="cmd">
        /// Команда</param>
        /// <param name="data">Строка с данными
        /// </param>
        /// <returns>
        /// Возвращает Model.Json с ответом
        /// </returns>
        public static Model.Json sendRequest(string type, string cmd, string data)
        {
            string url = baseUrl + cmd;
            Console.WriteLine(url + data);
            string respond = "";

            if (type == "GET")
            {
                respond = sendGET(url, data);
            }

            Model.Json json = JsonConvert.DeserializeObject<Model.Json>(respond);

            return json;
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

        private static string sendPOST(string Url, string Data)
        {
            WebRequest req = WebRequest.Create(Url);
            req.Method = "POST";
            req.Timeout = 100000;
            req.ContentType = "application/x-www-form-urlencoded";
            byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
            req.ContentLength = sentData.Length;
            Stream sendStream = req.GetRequestStream();
            sendStream.Write(sentData, 0, sentData.Length);
            sendStream.Close();
            WebResponse res = req.GetResponse();
            Stream ReceiveStream = res.GetResponseStream();
            StreamReader sr = new StreamReader(ReceiveStream, Encoding.UTF8);
            //Кодировка указывается в зависимости от кодировки ответа сервера
            Char[] read = new Char[256];
            int count = sr.Read(read, 0, 256);
            string Out = String.Empty;
            while (count > 0)
            {
                String str = new String(read, 0, count);
                Out += str;
                count = sr.Read(read, 0, 256);
            }
            return Out;
        }

        private static string sendGET(string url, string data)
        {
            WebRequest req = WebRequest.Create(url + data);
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



        public static void UploadFilesToRemoteUrl(string[] files)
        {
            string boundary = "----------------------------" + DateTime.Now.Ticks.ToString("x");


            HttpWebRequest httpWebRequest2 = (HttpWebRequest)WebRequest.Create(baseUrl + "put_file");
            httpWebRequest2.ContentType = "multipart/form-data; boundary=" + boundary;
            httpWebRequest2.Method = "POST";
            //httpWebRequest2.KeepAlive = true;
            //httpWebRequest2.Credentials = CredentialCache.DefaultCredentials;



            Stream memStream = new MemoryStream();

            byte[] boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");


            string formdataTemplate = "\r\n--" + boundary + "\r\nContent-Disposition: form-data; name=\"{0}\";\r\n\r\n{1}";


            NameValueCollection nvc = new NameValueCollection();
            nvc.Add("dir_id", "913992033028");
            nvc.Add("token", "2713c0b9d57bb5c736328cee93aaca2b");
            //nvc.Add("overwrite", "true");


            foreach (string key in nvc.Keys)
            {
                string formitem = string.Format(formdataTemplate, key, nvc[key]);
                byte[] formitembytes = System.Text.Encoding.UTF8.GetBytes(formitem);
                memStream.Write(formitembytes, 0, formitembytes.Length);
            }




            memStream.Write(boundarybytes, 0, boundarybytes.Length);

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n Content-Type: application/octet-stream\r\n\r\n";

            for (int i = 0; i < files.Length; i++)
            {
                string header = string.Format(headerTemplate, "file", files[i]);

                byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header);

                memStream.Write(headerbytes, 0, headerbytes.Length);


                FileStream fileStream = new FileStream(files[i], FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[1024];

                int bytesRead = 0;

                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }


                memStream.Write(boundarybytes, 0, boundarybytes.Length);


                fileStream.Close();
            }

            httpWebRequest2.ContentLength = memStream.Length;

            Stream requestStream = httpWebRequest2.GetRequestStream();

            memStream.Position = 0;
            byte[] tempBuffer = new byte[memStream.Length];
            memStream.Read(tempBuffer, 0, tempBuffer.Length);
            memStream.Close();
            requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            requestStream.Close();


            WebResponse webResponse2 = httpWebRequest2.GetResponse();

            //Stream stream2 = webResponse2.GetResponseStream();
            //StreamReader reader2 = new StreamReader(stream2);


            webResponse2.Close();
            httpWebRequest2 = null;
            webResponse2 = null;
        }
    }
}
