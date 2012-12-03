using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Model
{
    class Session
    {
        //поля
        private string email, lang, account, token;

        //конструкторы
        public Session(string email, string lang, string account, string token)
        {
            this.email = email;
            this.lang = lang;
            this.account = account;
            this.token = token;
        }


        //свойства
        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        public string Lang
        {
            get { return lang; }
            set { lang = value; }
        }
        public string Account
        {
            get { return account; }
            set { account = value; }
        }
        public string Token
        {
            get { return token; }
            set { token = value; }
        }
    }
}
