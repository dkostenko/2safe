using System;

namespace tosafe
{
	/// <summary>
	/// Сессия программы. Логин, пароль, токен и т.д. храняться только здесь.
	/// </summary>
	public class Session
	{
		string login;
		string password;
		string token;

		public Session ()
		{
			this.login = null;
			this.password = null;
			this.token = null;
		}

		public string Login {
			get { return login; }
			set { login = value; }
		}

		public string Password {
			get { return password; }
			set { password = value; }
		}

		public string Token {
			get { return token; }
			set { token = value; }
		}
	}
}

