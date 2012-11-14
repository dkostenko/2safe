using System;
using System.Runtime.Serialization.Json;

namespace tosafe
{
	class MainClass
	{
		public static void Main (string[] args)
		{

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
