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
using System.Data.SQLite;

namespace TwoSafe.View
{
    public partial class Test : Form
    {
        SQLiteConnection sqlitConnection;
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
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.auth("kostenko", "123qwe123qwe");
            Model.User.token = json["response"]["token"];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Controller.Synchronize.start();
            Dictionary<string, string> data = new Dictionary<string,string>();
            data.Add("dir_id","2285033047");
            data.Add("token",Model.User.token);
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.putFile(data, @"C:\Users\Public\Videos\Sample Videos\Wildlife.wmv");
            if (1 == 1)
            {
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            //Controller.Synchronize.doSync();
        }
    }
}
