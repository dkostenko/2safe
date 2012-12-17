using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Net.NetworkInformation;

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

        /// <summary>
        /// Проверяет есть ли активное соединение с интернетом
        /// </summary>
        /// <returns></returns>
        public static bool isOnline()
        {
            
            Ping myPing = new Ping();
            byte[] buffer = new byte[32];
            PingOptions pingOptions = new PingOptions();
            try
            {
                PingReply reply = myPing.Send("google.com", 1000, buffer, pingOptions);
                if (reply.Status == IPStatus.Success)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            
            return false;
        }

        public static bool userFolderExists()
        {
            if (Directory.Exists(userFolderPath))
            {
                return true;
            }
            return false;
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
