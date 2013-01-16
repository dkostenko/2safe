using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace TwoSafe.View
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            Model.Db.clearTables();
        }

        private void button2_Click(object sender, EventArgs e)
        {
        }


        private void button3_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(Controller.Synchronize.CloneServer);
            th.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Model.Db.create();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            DateTime d = File.GetLastWriteTimeUtc(Properties.Settings.Default.UserFolderPath + "\\666555.txt");
            DateTime r = File.GetLastWriteTime(Properties.Settings.Default.UserFolderPath + "\\666555.txt");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.auth("kostenko", "123qwe123qwe", null);
            Properties.Settings.Default.Token = json["response"]["token"];
            Properties.Settings.Default.Save();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Controller.Synchronize.MonitorChanges();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Controller.Synchronize.Start();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Controller.Synchronize.MonitorChanges();
        }
    }
}
