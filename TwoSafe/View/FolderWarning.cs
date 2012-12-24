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
    public partial class FolderWarning : Form
    {
        ResourceManager language;

        public FolderWarning()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            language = new ResourceManager(typeof(TwoSafe.View.WinFormStrings));
            InitializeComponent();
        }

        private void FolderWarning_Load(object sender, EventArgs e)
        {
            labelWarning.Text = language.GetString("message005") + @"C:\Users\" + Environment.UserName + @"\2safe" + language.GetString("message006");
        }
    }
}
