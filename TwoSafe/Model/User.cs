using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Runtime;
using System.Runtime.InteropServices;


namespace TwoSafe.Model
{
    public static class User
    {
        public static string token;
        public static string userFolderPath;

        // Статический конструктор вызывается при первом обращении к любому члену класса
        static User()
        {
            token = Properties.Settings.Default.Token;
            userFolderPath = Properties.Settings.Default.UserFolderPath;
        }

        //Creating the extern function...
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Проверяет есть ли активное соединение с интернетом
        /// </summary>
        /// <returns></returns>
        public static bool isOnline()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        /// <summary>
        /// Проверяет токен на валидность. В случае невалидного токена возращает FALSE
        /// </summary>
        public static bool isAuthorized()
        {
            
            try
            {
                Dictionary<string, dynamic> response = Controller.ApiTwoSafe.getPersonalData(token);

                if (response.ContainsKey("error_code"))
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            } 
        }



    }
}
