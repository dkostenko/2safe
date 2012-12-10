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
            Dictionary<string, dynamic> response = Controller.ApiTwoSafe.auth(tbAccount.Text, tbPassword.Text);

            if (response.ContainsKey("error_code"))
            {
                MessageBox.Show(language.GetString("error" + response["error_code"]));
            }
            else
            {
                this.cookie[0] = response["response"]["token"];
                formPreferences.Show();
                this.Close();
            }
        }
    }
}
