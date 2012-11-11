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
			string url = baseUrl + cmd + data;
			string respond = "";

			if (type == "GET") {
				respond = sendGet(url,data);
			}

			return respond;
		}

		private static string sendGet(string Url, string Data)
		{
			System.Net.WebRequest req = System.Net.WebRequest.Create(Url + "?" + Data);
			System.Net.WebResponse resp = req.GetResponse();
			System.IO.Stream stream = resp.GetResponseStream();
			System.IO.StreamReader sr = new System.IO.StreamReader(stream);
			string Out = sr.ReadToEnd();
			sr.Close();
			return Out;
		}
	}
}

