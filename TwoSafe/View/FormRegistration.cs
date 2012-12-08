using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Globalization;
using System.Resources;

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
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> response = Controller.ApiTwoSafe.addLogin(tbAccount.Text, tbPassword.Text, tbEmail.Text, tbCaptcha.Text, captchaId);

            if (response.ContainsKey("error_code"))
            {
                MessageBox.Show(response["error_msg"]);
            }
            else
            {
                MessageBox.Show(response["response"]["success"]);
            }
        }

        private void FormRegistration_Shown(object sender, EventArgs e)
        {
            //показываем капчу
            Thread newTh = new Thread(getCaptchaAndId);
            newTh.Start();
        }

        private void getCaptchaAndId()
        {
            Object[] captcha = Controller.ApiTwoSafe.getCaptcha();
            pbCaptcha.BackgroundImage = (Bitmap)captcha[0];
            captchaId = (string)captcha[1];
        }
    }
}
