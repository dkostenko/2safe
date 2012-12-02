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

            //смотрим, атворизован ли пользователь

            //если да, то открываем программу и засовываем в трей
            //Application.Run(new FormLogin());
            Application.Run(new View.FormRegistration());

            //если нет, то открываем форму логина
        }
    }
}
