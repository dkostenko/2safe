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
    public partial class FormLogin : Form
    {
        private string[] cookie;
        private View.FormPreferences formPreferences;
        ResourceManager language;

        public FormLogin(string[] cookie, View.FormPreferences formPreferences)
        {
            InitializeComponent();
            this.cookie = cookie;
            this.formPreferences = formPreferences;
            this.language = new ResourceManager("TwoSafe.View.WinFormStrings", typeof(FormPreferences).Assembly);
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            string data = "&login=" + tbAccount.Text + "&password=" + tbPassword.Text;
            Model.Json json = Controller.Connection.sendRequest("GET", "auth", data);
            if (json.error_code != null)
            {
                MessageBox.Show(language.GetString("error" + json.error_code));
                return;
            }
            if (json.response.success != null && json.response.success)
            {
                this.cookie[0] = json.response.token;
                Model.Cookie.Write(cookie);
                formPreferences.Show();
                this.Close();
            }
        }
    }
}
