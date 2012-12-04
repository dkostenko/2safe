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
        private string[] cookie;
        private View.FormPreferences formPreferences;
        public FormLogin(string[] cookie, View.FormPreferences formPreferences)
        {
            InitializeComponent();
            this.cookie = cookie;
            this.formPreferences = formPreferences;
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string data = "&login=" + tbAccount.Text + "&password=" + tbPassword.Text;
            Model.Json json = Controller.Connection.sendRequest("GET", "auth", data);
            if (json.response.success)
            {
                this.cookie[3] = json.response.token;
                Model.Cookie.Write(json.response.token);
                formPreferences.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Вы не вошли");
            }
            
        }
    }
}
