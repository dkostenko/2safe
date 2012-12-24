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
        ResourceManager language;

        public FormLogin()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            language = new ResourceManager(typeof(TwoSafe.View.WinFormStrings));
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> response = Controller.ApiTwoSafe.auth(tbAccount.Text, tbPassword.Text, "", "");

            if (response.ContainsKey("error_code"))
            {
                labelErrorMessage.Text = language.GetString("error" + response["error_code"]);
            }
            else
            {
                Properties.Settings.Default.Token = response["response"]["token"];
                Properties.Settings.Default.Save();
                this.Close();
            }
        }
    }
}
