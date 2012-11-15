using System;
using System.IO;
using System.Net;
using System.Text;

namespace tosafe
{
	public static class Connection
	{
		private static string baseUrl = "https://api.2safe.com/?cmd=";

		/// <summary>
		/// Отправляет запросы GET и POST
		/// </summary>
		/// <param name="type">
		/// тип запроса. POST или GET
		/// </param>
		/// <param name="cmd">
		/// Команда</param>
		/// <param name="data">Строка с данными
		/// </param>
		/// <returns>
		/// Возвращает JSON с ответом
		/// </returns>
		public static string sendRequest (string type, string cmd, string data)
		{
			string url = baseUrl + cmd;
			Console.WriteLine(url + data);
			string respond = "";

			if (type == "GET") {
				respond = sendGET(url,data);
			}

			return respond;
		}

		private static string sendPOST(string Url, string Data)
		{
			System.Net.WebRequest req = System.Net.WebRequest.Create(Url);
			req.Method = "POST";
			req.Timeout = 100000;
			req.ContentType = "application/x-www-form-urlencoded";
			byte[] sentData = Encoding.GetEncoding(1251).GetBytes(Data);
			req.ContentLength = sentData.Length;
			System.IO.Stream sendStream = req.GetRequestStream();
			sendStream.Write(sentData, 0, sentData.Length);
			sendStream.Close();
			System.Net.WebResponse res = req.GetResponse();
			System.IO.Stream ReceiveStream = res.GetResponseStream();
			System.IO.StreamReader sr = new System.IO.StreamReader(ReceiveStream, Encoding.UTF8);
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
			try {
				WebResponse resp = req.GetResponse();
				Stream stream = resp.GetResponseStream();
				StreamReader sr = new StreamReader(stream);
				output = sr.ReadToEnd();
				sr.Close();
			}
			catch (WebException wex){
				if (wex.Response != null) {
					using (var errorResponse = (HttpWebResponse)wex.Response) {
						using (var reader = new StreamReader(errorResponse.GetResponseStream())) {
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
