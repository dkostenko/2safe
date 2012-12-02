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

namespace TwoSafe.View
{
    public partial class FormRegistration : Form
    {
        public FormRegistration()
        {
            InitializeComponent();
            string respond = Controller.Connection.sendRequest("GET", "get_captcha", "");
            MessageBox.Show(respond);
        }

        private void buttonRegistration_Click(object sender, EventArgs e)
        {
            string data = "&login=" + tbAccount.Text + "&password=" + tbPassword.Text + "&email=" + tbEmail.Text;
            string respond = Controller.Connection.sendRequest("GET", "add_login", data);
            MessageBox.Show(respond);
            Model.Json json = JsonConvert.DeserializeObject<Model.Json>(respond);
        }
    }
}
