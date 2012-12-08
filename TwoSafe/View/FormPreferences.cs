using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Specialized;
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

            if (this.cookie == null) //если куков не существует, то просим пользователя авторизоваться заного
            {
                showLoginForm = true;
            }
            else
            {
                cookie = Model.Cookie.Read();

                Dictionary<string, dynamic> response = Controller.ApiTwoSafe.getPersonalData(cookie[0]);

                if (response.ContainsKey("error_code"))
                {
                    MessageBox.Show(response["error_msg"]);
                }
                else
                {
                    MessageBox.Show(response["response"]["success"]);
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
            //NameValueCollection postData = new NameValueCollection();
            //postData.Add("token", "4233308f69da003a7d19cbda751a32f4");
            //postData.Add("dir_id", "1074797033539"); 

            //Dictionary<string, dynamic> respond = Controller.ApiTwoSafe.putFile(postData, "C:/Users/dmitry/Desktop/text__.txt");

            //Controller.ApiTwoSafe.getFile("1931301033537", "4233308f69da003a7d19cbda751a32f4", null, "C:/Users/dmitry/Desktop/asd.jpg");

            //Controller.ApiTwoSafe.removeFile("1931301033537", "4233308f69da003a7d19cbda751a32f4", false);
        }
    }
}
