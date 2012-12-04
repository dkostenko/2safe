using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoSafe
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //посылаем куки в форму настроек (далее Главная форма)
            //если куки пустые, то отроется форма авторизации

            string[] cookie = Model.Cookie.Read();

            Application.Run(new View.FormPreferences(cookie));
        }
    }
}
