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

        public FormPreferences()
        {
            InitializeComponent();
            Thread.CurrentThread.CurrentCulture = new CultureInfo("en-US"); // эта строка ициниализирует язык ru-RU
            this.cookie = Model.Cookie.Read();
            
            //trayIcon.Visible = true; // показываем иконку в трее
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
            bool showLoginForm = false;
            Model.Json json;

            if (this.cookie == null) //если куков не существует, то просим пользователя авторизоваться заного
            {
                showLoginForm = true;
            }
            else
            {
                cookie = Model.Cookie.Read();
                json = Controller.Connection.sendRequest("GET", "get_personal_data", "&token=" + cookie[0]);
                if (json.error_code != null)
                {
                    showLoginForm = true;
                }
            }

            if(showLoginForm)
            {
                cookie = new string[5];
                FormLogin formLogin = new FormLogin(cookie, this);
                this.Hide();
                formLogin.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] files = new string[1];
            files[0] = "C:/Users/dmitry/Desktop/text__.txt";
            Controller.Connection.UploadFilesToRemoteUrl(files);
        }
    }
}
