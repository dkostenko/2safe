using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwoSafe.View
{
    public partial class FormPreferences : Form
    {
        private string[] cookie;

        public FormPreferences(string[] cookie)
        {
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
    }
}
