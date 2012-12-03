using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TwoSafe.Model
{
    class Cookie
    {

        /// <summary>
        /// Записать cookie
        /// </summary>
        /// <param name="args"> Массив строк с аргументами. [0] - токен </param>
        public static void Write(string[] args)
        {
            StreamWriter sw = new StreamWriter("Cookie.txt", false);
            for (int i = 0; i < args.Length; i++)
            {
                sw.WriteLine(args[i] + "\n");
            }
            sw.Close();
        }

        /// <summary>
        /// Записать только токен
        /// </summary>
        /// <param name="token"> токен </param>
        public static void Write(string token)
        {
            StreamWriter sw = new StreamWriter("Cookie.txt", false);
            sw.WriteLine(token);
            sw.Close();
        }

        /// <summary>
        /// Читать cookie
        /// </summary>
        /// <returns> Массив строк. [0] - токен </returns>
        public static string[] Read()
        {
            try
            {
                StreamReader sr = new StreamReader("Cookie.txt");
                string textFromFile = sr.ReadToEnd();
                sr.Close(); 
                char[] separators = new char[] { '\n' };
                string[] cookie = textFromFile.Split(separators, StringSplitOptions.RemoveEmptyEntries);
                if (cookie.Length == 0) {return null; }
                return cookie;
            }
            catch
            {
                return null;
            }    
        }
    }    
}

