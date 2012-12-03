using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwoSafe.Model
{
    class UserInfo
    {
       
        /// <summary>
        /// Писать token в файл
        /// </summary>
        /// <param name="token"> токен </param>
        public static void WriteTokenToFile(string token)
        {
            StreamWriter sw = new StreamWriter("token.txt", false);
            sw.WriteLine(token);
            sw.Close();
        }
        
        /// <summary>
        /// Читать токен из файла
        /// </summary>
        /// <returns> token </returns>
        public static string ReadTokenFromFile()
        {
            StreamReader sr = new StreamReader("token.txt");
            string token = sr.ReadToEnd();
            sr.Close();
            return token;
            
        }

        public static Boolean checkCurrentTokenForValidity()
        {
            if (File.Exists("userInfo.txt"))
            {
                StreamReader sr = new StreamReader("token.txt");
                string token = sr.ReadToEnd();
                //TODO: Проверить - будет ли успешной какая нибудь команда с токеном
                sr.Close();
            }
            return false;
        }
    }
}
