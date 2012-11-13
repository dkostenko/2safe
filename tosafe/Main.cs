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

//			Console.WriteLine ("Hello World!!");
//			string cmd = "chk_mail";
//			string data = "&email=kostenko.market@gmail.com";
//
//			string respond = Connection.sendRequest("GET", cmd, data);
//			Console.WriteLine("string respond = " + respond);
//			RootObject resp = JsonConvert.DeserializeObject<RootObject>(respond);
//			Console.WriteLine("===========");
//			Console.WriteLine("success = " + resp.response.success);
//			Console.WriteLine("available = " + resp.response.available);
//
//
//			Console.WriteLine("Для завершения программы нажмите ENTER");
//
//
//
//			Console.ReadLine();


			ConsoleInterface ci = new ConsoleInterface();
			ci.Run();
		
		}
	}

	class ConsoleInterface
	{
		string mainMenu = "\nMAIN MENU\n\n1.     Проверить доступность email\n2.     Проверить доступность login\n3.     Авторизоваться\nESC.   Выход\n\nНажмите соответствующую клавишу";
		string emailMessage = "\nВведите email который хотите проверить на доступность: ";
		string loginMessage = "\nВведите login который хотите проверить на доступность: ";
		string loginPrompt = "\nВведите логин :";
		string passPrompt = "\nВведите пароль :";
		string cmd, data, respond;

		bool flag = true;


		public void Run ()
		{


			while (flag) 
			{
				Console.Clear ();
				Console.Write (mainMenu);
				ConsoleKeyInfo key = Console.ReadKey();

				switch (key.Key)
				{
				case (ConsoleKey.Escape):	
					flag = false;
					break;
				case (ConsoleKey.D1):
					Console.Clear();
					Console.Write(emailMessage);
					cmd = "chk_mail";
					data = "&email=" + Console.ReadLine();
					respond = Connection.sendRequest("GET", cmd, data);
					Console.WriteLine(respond);
					Console.ReadLine();
					break;
				case (ConsoleKey.D2):
					Console.Clear();
					Console.Write(loginMessage);
					cmd = "chk_login";
					data = "&login=" + Console.ReadLine();
					respond = Connection.sendRequest("GET", cmd, data);
					Console.WriteLine(respond);
					Console.ReadLine();
					break;
				case (ConsoleKey.D3):
					Console.Clear();
					Console.Write(loginPrompt);
					cmd = "auth";
					data = "&login=" + Console.ReadLine();
					Console.Write(passPrompt);
					data += "&password=" + Console.ReadLine();
					respond = Connection.sendRequest("GET", cmd, data);
					Console.WriteLine(respond);
					Console.ReadLine();
					break;
				}
			}
		}
	}
}
