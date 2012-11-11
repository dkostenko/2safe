using System;

namespace tosafe
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Console.WriteLine ("Hello World!!");
			string cmd = "chk_mail";
			string data = "&email=kostenko.market@gmail.com";

			string respond = Connection.sendRequest("GET", cmd, data);
			Console.WriteLine(respond);

			Console.WriteLine("Для завершения программы нажмите ENTER");
			Console.ReadLine();
		}
	}
}
