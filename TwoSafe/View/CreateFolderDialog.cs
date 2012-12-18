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
    public partial class CreateFolderDialog : Form
    {
        public CreateFolderDialog()
        {
            InitializeComponent();
            buttonExit.DialogResult           = DialogResult.Abort;
            buttonCreateDefault.DialogResult  = DialogResult.OK;
            buttonCreateCustom.DialogResult   = DialogResult.Yes;
        }
    }
}
