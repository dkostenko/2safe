﻿using System;
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
    public partial class FormPreferences : Form
    {
        public FormPreferences()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            View.FormRegistration formRegistration = new FormRegistration();
            formRegistration.Show();
            //this.Hide();
        }
    }
}
