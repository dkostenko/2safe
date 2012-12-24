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
        //Creating the extern function...
        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int Description, int ReservedValue);

        /// <summary>
        /// Проверяет есть ли активное соединение с интернетом
        /// </summary>
        /// <returns> TRUE если активно интернет-соединение, FALSE если оно неактивно </returns>
        public static bool isOnline()
        {
            int Desc;
            return InternetGetConnectedState(out Desc, 0);
        }

        /// <summary>
        /// Метод для проверки токена на правильность
        /// </summary>
        /// <returns> TRUE при активном интернет-соединении и валидном токене, FALSE во всех остальных случаях </returns>
        public static bool isAuthorized()
        {
            try
            {
                Dictionary<string, dynamic> json = Controller.ApiTwoSafe.getPersonalData();

                if (json.ContainsKey("error_code"))
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
