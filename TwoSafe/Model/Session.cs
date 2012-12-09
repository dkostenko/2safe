using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Model
{
    static class Session
    {
        //поля
        private static string email, lang, account, token, mainFolderPath;

        /// <summary>
        /// Начинает сессию
        /// </summary>
        public static void startSession(string email, string lang, string account, string token, string mainFolderPath)
        {
            Email = email;
            Lang = lang;
            Account = account;
            Token = token;
            MainFolderPath = mainFolderPath;
        }


        //свойства
        public static string Email
        {
            get { return email; }
            set { email = value; }
        }
        public static string Lang
        {
            get { return lang; }
            set { lang = value; }
        }
        public static string Account
        {
            get { return account; }
            set { account = value; }
        }
        public static string Token
        {
            get { return token; }
            set { token = value; }
        }
        public static string MainFolderPath
        {
            get { return mainFolderPath; }
            set { mainFolderPath = value; }
        }
    }
}
