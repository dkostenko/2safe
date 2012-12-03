using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Threading;

namespace TwoSafe.View
{
    public partial class FormRegistration : Form
    {
        private string captchaId;

        public FormRegistration()
        {
            captchaId = "";
            InitializeComponent();
            //TODO показать картинку-заглушку на месте капчи, пока капча подгружается
            //НАЧАЛО ТЕСТОВ
            Model.Cookie.Write(new string[] {"h2k1jh21kh21","whatever thi s s","man fuck you"});
            string[] temp = Model.Cookie.Read();
            for (int i = 0; i < temp.Length; i++)
            {
                tbAccount.AppendText(temp[i] + " ");
            }
            //Конец тестов

        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            string data = "&login=" + tbAccount.Text + 
                          "&password=" + tbPassword.Text + 
                          "&email=" + tbEmail.Text + 
                          "&captcha=" + tbCaptcha.Text + 
                          "&id=" + captchaId;
            Model.Json json = Controller.Connection.sendRequest("GET", "add_login", data);
            MessageBox.Show(json.response.success.ToString());
        }

        private void FormRegistration_Shown(object sender, EventArgs e)
        {
            //показываем капчу
            Thread newTh = new Thread(getCaptchaAndId);
            newTh.Start();
        }

        private void getCaptchaAndId()
        {
            Object[] captcha = Controller.Connection.getCaptcha();
            pbCaptcha.BackgroundImage = (Bitmap)captcha[0];
            captchaId = (string)captcha[1];
        }
    }
}
