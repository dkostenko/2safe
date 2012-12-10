using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwoSafe.Model
{
    public static class User
    {
        public static string token = "";

        static User()
        {
            token = "";
        }


        /// <summary>
        /// Проверяет токен на валидность. В случае невалидного токена возращает FALSE
        /// </summary>
        public static bool isAuthorized()
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

                Dictionary<string, dynamic> response = Controller.ApiTwoSafe.getPersonalData(cookie[0]);

                if (response.ContainsKey(response["error_code"]))
                {
                    return false;
                }

                token = cookie[0];

                return true;
            }
            catch
            {
                return false;
            } 
        }

        /// <summary>
        /// Сохраняет токен в локальном файле
        /// </summary>
        public static void saveToken()
        {
            StreamWriter sw = new StreamWriter("Cookie.txt", false);
            sw.WriteLine(token);
            sw.Close();
        }
    }
}
