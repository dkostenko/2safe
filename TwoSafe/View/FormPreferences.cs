using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Globalization;
using System.Threading;
using System.Resources;

namespace TwoSafe.View
{
    public partial class FormPreferences : Form
    {
        private string[] cookie;

        public FormPreferences(string[] cookie)
        {
            
           Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US"); // эта строка ициниализирует язык ru-RU
            InitializeComponent();
            this.cookie = cookie;

            trayIcon.Visible = true; // показываем иконку в трее
        }

        private void button1_Click(object sender, EventArgs e)
        {
            View.FormRegistration formRegistration = new FormRegistration();
            formRegistration.Show();
            //this.Hide();
        }

        private void FormPreferences_FormClosing(object sender, FormClosingEventArgs e)
        {
            //e.Cancel = true;
            //this.Hide();
        }

        private void notifyIcon_Click(object sender, EventArgs e)
        {
        }

        private void FormPreferences_Shown(object sender, EventArgs e)
        {
            if (this.cookie == null) //если куков не существует, то просим пользователя авторизоваться заного
            {
                cookie = new string[5];
                this.Hide();
                FormLogin formLogin = new FormLogin(cookie, this);
                formLogin.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ResourceManager Lan = new ResourceManager("TwoSafe.WinFormStrings", typeof(FormPreferences).Assembly);
            MessageBox.Show(Lan.GetString("testvar"));
        }

    }
}
