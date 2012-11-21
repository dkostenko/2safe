using System;
using Newtonsoft.Json;

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
		string mainMenu = "\nMAIN MENU\n\n" +
			"1.Авторизоваться\n" +
			"2.Показать сессию\n" +
			"3.Выйти\n" +
			"4.Карточка пользователя\n" +
			"5.Создание директории\n" +
			"ESC.Выход\n\n" +
			"Нажмите соответствующую клавишу";

		string loginPrompt = "\nВведите логин :";
		string passPrompt = "\nВведите пароль :";
		string cmd, data, respond;

		bool flag = true;


		public void Run ()
		{

			Session session = new Session();
			Json json = null;

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
					string password;
					Console.Clear();
					Console.Write(loginPrompt);
					cmd = "auth";
					session.Login = Console.ReadLine();
					data = "&login=" + session.Login;
					Console.Write(passPrompt);
					password = Console.ReadLine();
					data += "&password=" + password;
					respond = Connection.sendRequest("GET", cmd, data);

					Console.WriteLine("string respond = " + respond);
					json = JsonConvert.DeserializeObject<Json>(respond);
					Console.WriteLine("===========");

					session.Token = json.response.token;
					Console.ReadLine();
					break;
				case (ConsoleKey.D2):
					Console.Clear();
					Console.WriteLine(session.ToString());
					Console.ReadLine();
					break;
				case (ConsoleKey.D3):
					Console.Clear();
					cmd = "logout";
					data = "&token=" + session.Token;
					respond = Connection.sendRequest("GET", cmd, data);
					Console.WriteLine("string respond = " + respond);
					json = JsonConvert.DeserializeObject<Json>(respond);

					if(json.error_msg != null)
					{
						Console.WriteLine("Все очень плохо");
					}
					else 
					{
						Console.WriteLine("Вы успешно вышли!");
						session = new Session();
						Console.WriteLine(session.ToString());
					}

					Console.ReadLine();
					break;
				case (ConsoleKey.D4):
					Console.Clear();
					cmd = "get_personal_data";
					data = "&token=" + session.Token;
					respond = Connection.sendRequest("GET", cmd, data);
					Console.WriteLine("string respond = " + respond);
					json = JsonConvert.DeserializeObject<Json>(respond);
					Console.WriteLine(json.response.personal.ToString());
					Console.ReadLine();
					break;
				case (ConsoleKey.D5):
					Console.Clear();
					cmd = "make_dir";
					data = "&dir_id=11";
					data += "&dir_name=newPapka";
					data += "&token=" + session.Token;
					respond = Connection.sendRequest("GET", cmd, data);
					Console.WriteLine("string respond = " + respond);
					Console.ReadLine();
					break;
				}
			}
		}
	}
}
