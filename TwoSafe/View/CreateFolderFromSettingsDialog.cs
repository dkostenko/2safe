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

namespace TwoSafe.View
{
    public partial class CreateFolderFromSettingsDialog : Form
    {
        public CreateFolderFromSettingsDialog()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            labelMessage.Text = Properties.Settings.Default.UserFolderPath;
            buttonCreateFolder.DialogResult = DialogResult.OK;
            buttonExit.DialogResult = DialogResult.Abort;
        }
    }
}
