using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwoSafe.Model
{
    public static class Session
    {
        private static string lang, token;

        /// <summary>
        /// Начинает сессию. Если возвращает False - необходимо авторизироваться заново. True - сессия началась удачно.
        /// </summary>
        public static bool startSession()
        {
            try
            {
                StreamReader sr = new StreamReader("Cookie.txt");
                string textFromFile = sr.ReadToEnd();
                sr.Close();
                char[] separators = new char[] { '\n' };
                string[] cookie = textFromFile.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (cookie.Length == 0) 
                { 
                    return false; 
                }

                lang = cookie[1];
                Dictionary<string, dynamic> response = Controller.ApiTwoSafe.getPersonalData(cookie[0]);

                if (response.ContainsKey(response["error_code"]))
                {
                    return false;
                }

                token = cookie[0];
                lang = response["response"]["personal"]["lang"];

                return true;
            }
            catch
            {
                return false;
            }    
        }

        /// <summary>
        /// Если не получилось начать сессию без авторизации, то необходимо "обновить" сессию валидным токеном?
        /// </summary>
        public static void refreshSession(string token)
        {
            if (Lang == null)
            {
                Lang = "ru";
            }
            Token = token;
            saveSession();
        }

        /// <summary>
        /// Сохраняет сессию в локальный файл. Записывает только токен и текущий язык программы
        /// </summary>
        public static void saveSession()
        {
            StreamWriter sw = new StreamWriter("Cookie.txt", false);
            sw.WriteLine(Token);
            sw.WriteLine(Lang);
            sw.Close();
        }


        /// <summary>
        /// Текущий язык программы
        /// </summary>
        public static string Lang
        {
            get { return lang; }
            set { lang = value; }
        }

        /// <summary>
        /// Валидный токен
        /// </summary>
        public static string Token
        {
            get { return token; }
            set { token = value; }
        }
    }
}
