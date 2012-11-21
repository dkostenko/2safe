using System;

namespace tosafe
{
	/// <summary>
	/// Сессия программы. Логин, токен и т.д. храняться только здесь.
	/// </summary>
	public class Session
	{
		string login;
		string token;

		public Session ()
		{
			this.login = null;
			this.token = null;
		}


		/// <summary>
		/// Выводит всю информацию об объекте.
		/// </summary>
		public override string ToString ()
		{
			return string.Format ("[Session: Login={0}, Token={1}]", Login, Token);
		}

		public string Login {
			get { return login; }
			set { login = value; }
		}

		public string Token {
			get { return token; }
			set { token = value; }
		}
	}
}

