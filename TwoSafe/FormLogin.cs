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

namespace TwoSafe
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string data = "&login=" + tbAccount.Text + "&password=" + tbPassword.Text;
            string respond = Controller.Connection.sendRequest("GET", "auth", data);
            Model.Json json = JsonConvert.DeserializeObject<Model.Json>(respond);
            if (json.response.success)
            {
                MessageBox.Show("Вы вошли");
                View.FormPreferences prefs = new View.FormPreferences();
                this.Close();
                prefs.Show();
            }
            else
            {
                MessageBox.Show("Вы не вошли");
            }
            
        }
    }
}
