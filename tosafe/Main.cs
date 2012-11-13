using System;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;

namespace tosafe
{
	public class Response
	{
		public string success { get; set; }
		public string available { get; set; }
	}
	
	public class RootObject
	{
		public Response response { get; set; }
	}


	class MainClass
	{
		public static void Main (string[] args)
		{

			Console.WriteLine ("Hello World!!");
			string cmd = "chk_mail";
			string data = "&email=kostenko.market@gmail.com";

			string respond = Connection.sendRequest("GET", cmd, data);
			Console.WriteLine("string respond = " + respond);
			RootObject resp = JsonConvert.DeserializeObject<RootObject>(respond);
			Console.WriteLine("===========");
			Console.WriteLine("success = " + resp.response.success);
			Console.WriteLine("available = " + resp.response.available);


			Console.WriteLine("Для завершения программы нажмите ENTER");



			Console.ReadLine();
		}
	}
}
