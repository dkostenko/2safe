﻿using System;
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

namespace TwoSafe.View
{
    public partial class CreateFolderDialog : Form
    {
        public CreateFolderDialog()
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(Properties.Settings.Default.Language);
            InitializeComponent();
            buttonExit.DialogResult           = DialogResult.Cancel;
            buttonCreateDefault.DialogResult  = DialogResult.OK;
            buttonCreateCustom.DialogResult   = DialogResult.Yes;
            
        }

    }
}
