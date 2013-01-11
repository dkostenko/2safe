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
            Controller.Dirs.syncDirsWithDb();
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

        private void button5_Click(object sender, EventArgs e)
        {
            Controller.Synchronize.fromServerToClient();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.auth("kostenko", "123qwe123qwe", null);
            Properties.Settings.Default.Token = json["response"]["token"];
            Properties.Settings.Default.Save();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            //события папки
            FileSystemWatcher dirWatcher = new FileSystemWatcher();
            dirWatcher.Path = Properties.Settings.Default.UserFolderPath;
            dirWatcher.IncludeSubdirectories = true;
            dirWatcher.NotifyFilter = NotifyFilters.DirectoryName;
            dirWatcher.Created += new FileSystemEventHandler(Controller.Dirs.eventHendler);
            dirWatcher.Deleted += new FileSystemEventHandler(Controller.Dirs.eventHendler);
            dirWatcher.EnableRaisingEvents = true;


            //события файла
            FileSystemWatcher fileWatcher = new FileSystemWatcher();
            fileWatcher.Path = Properties.Settings.Default.UserFolderPath;
            fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            fileWatcher.Created += new FileSystemEventHandler(Controller.Synchronize.eventRaised);
            //fileWatcher.Changed += new FileSystemEventHandler(Controller.Synchronize.eventRaised);
            //fileWatcher.Deleted += new FileSystemEventHandler(Controller.Synchronize.eventRaised);
            //fileWatcher.Renamed += new RenamedEventHandler(Controller.Synchronize.eventRaised);
            //fileWatcher.EnableRaisingEvents = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            int i, j;
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.getEvents("1356556645");
            Dictionary<string, dynamic> tempJSON;
            System.Collections.ArrayList tempArray;
            Model.Dir curentDir;
            string[] subDirs;
            List<Model.Dir> que = new List<Model.Dir>();
            List<Model.Dir> ImmuneDirs = new List<Model.Dir>();

            List<Model.Dir> s_dir_add = new List<Model.Dir>();
            List<Model.Dir> s_dir_del = new List<Model.Dir>();
            List<Model.Dir> LD_dir_add = new List<Model.Dir>();
            List<Model.Dir> LD_dir_del = new List<Model.Dir>();

            foreach (var one in json["response"]["events"])
            {

                if (one["event"] == "dir_created")
                {
                    s_dir_add.Add(new Model.Dir(one["id"], one["parent_id"], one["name"]));
                    continue;
                }
                if (one["event"] == "dir_moved" && one["new_parent_id"] == "913990033028")
                {
                    s_dir_del.Add(new Model.Dir(one["id"], one["old_parent_id"], one["old_name"]));
                    continue;
                }
            }

            //создаем чистый список добавлений папок на сервере
            //Добавляем каждую папку и родительские папки в ImmuneDirs
            for (i = 0; i < s_dir_add.Count; ++i)
            {
                tempJSON = Controller.ApiTwoSafe.getProps(s_dir_add[i].Id.ToString())["response"]["object"];
                if (!tempJSON.ContainsKey("is_trash"))
                {
                    s_dir_add.Remove(s_dir_add[i]);

                    if (tempJSON["parent_id"] == Properties.Settings.Default.RootId.ToString())
                    {
                        ImmuneDirs.Add(new Model.Dir(tempJSON["id"], tempJSON["parent_id"], tempJSON["name"]));
                    }
                    else
                    {
                        tempArray = Controller.ApiTwoSafe.getTreeParent(tempJSON["parent_id"])["response"]["tree"];
                        for (j = 0; j < tempArray.Count - 1; ++j)
                        {
                            tempJSON = (Dictionary<string, dynamic>)tempArray[j];
                            ImmuneDirs.Add(new Model.Dir(tempJSON["id"], tempJSON["parent_id"], tempJSON["name"]));
                        }
                    }
                }
            }
            //создаем чистый список удалений папок на сервере
            //Удаляем каждую папку из s_file_del, которая содержится в ImmuneDirs
            for (i = 0; i < s_dir_del.Count; ++i)
            {
                for (j = 0; j < ImmuneDirs.Count; ++j)
                {
                    if (s_dir_del[i].Id == ImmuneDirs[j].Id)
                    {
                        s_dir_del.Remove(s_dir_del[i]);
                        break;
                    }
                }
            }



            //2) применим чистые добавления сервера к БД и LD
            for (i = 0; i < s_dir_add.Count; ++i)
            {
                s_dir_add[i].Save();
                Directory.CreateDirectory(s_dir_add[i].Path);
            }




            //3) получить список изменений в LD
            
            subDirs = Directory.GetDirectories(Properties.Settings.Default.UserFolderPath);
            for (i = 0; i < subDirs.Length; ++i)
            {
                curentDir = Model.Dir.FindByNameAndParentId(Path.GetFileName(subDirs[i]), Properties.Settings.Default.RootId, true);
                if (curentDir == null)
                {
                    tempJSON = Controller.ApiTwoSafe.makeDir(Properties.Settings.Default.RootId, Path.GetFileName(subDirs[i]), null);
                    curentDir = new Model.Dir(tempJSON["response"]["dir_id"], Properties.Settings.Default.RootId, Path.GetFileName(subDirs[i]));
                    curentDir.Save();
                }
                que.Add(curentDir);
            }


            while (que.Count > 0)
            {
                if (Directory.Exists(que[0].Path))
                {
                    subDirs = Directory.GetDirectories(que[0].Path);

                    for (i = 0; i < subDirs.Length; ++i)
                    {
                        curentDir = Model.Dir.FindByNameAndParentId(Path.GetFileName(subDirs[i]), que[0].Id.ToString(), true);
                        if (curentDir == null)
                        {
                            tempJSON = Controller.ApiTwoSafe.makeDir(que[0].Id.ToString(), Path.GetFileName(subDirs[i]), null);
                            curentDir = new Model.Dir(tempJSON["response"]["dir_id"], que[0].Id.ToString(), Path.GetFileName(subDirs[i]));
                            curentDir.Save();
                            LD_dir_add.Add(curentDir);
                        }
                        que.Add(curentDir);
                    }
                }
                else
                {
                    LD_dir_del.Add(que[0]);
                }

                que.Remove(que[0]);
            }


        }

        private void button9_Click(object sender, EventArgs e)
        {

        }
    }
}
