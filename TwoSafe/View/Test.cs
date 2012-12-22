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

            FileSystemWatcher watcher = new FileSystemWatcher();
            watcher.Path = "C:\\Users\\dmitry\\Desktop";
            //watcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;

            // Подписка на обработчик события изменения файла или папки
            watcher.Created += new FileSystemEventHandler(Controller.Synchronize.eventRaised);
            watcher.Changed += new FileSystemEventHandler(Controller.Synchronize.eventRaised);
            watcher.Deleted += new FileSystemEventHandler(Controller.Synchronize.eventRaised);
            watcher.Renamed += new RenamedEventHandler(Controller.Synchronize.eventRaised);

            try
            {
                watcher.EnableRaisingEvents = true;
            }
            catch (ArgumentException)
            {
                //abortAcitivityMonitoring();
            }


            //SQLiteConnection.CreateFile("twoSafe.sqlite"); 


           // Model.TwoSafeDB.createTable();

          

        }


        private void button1_Click(object sender, EventArgs e)
        {
            Model.Db.clearTables();
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.auth("kostenko", "123qwe123qwe");
            Model.User.token = json["response"]["token"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(qwe);
            th.Start();
        }

        static void qwe()
        {
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.putFile("28101033560", @"C:\Users\dmitry\Desktop\qwe.txt", null);
            if (1 == 1)
            {
                //Properties.Settings.Default.Token;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Thread th = new Thread(Controller.Synchronize.inception);
            th.Start();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Model.Db.create();
        }
    }
}
