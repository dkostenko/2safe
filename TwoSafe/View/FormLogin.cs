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
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string data = "&login=" + tbAccount.Text + "&password=" + tbPassword.Text;
            Model.Json json = Controller.Connection.sendRequest("GET", "auth", data);
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
