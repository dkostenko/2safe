using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TwoSafe.Controller
{
    class Dirs
    {

        public static void CreateOnClient(string id, string parent_id, string name)
        {
            //Model.Dir dir = new Model.Dir(id, parent_id, name);
            //dir.Save();

            string path = Properties.Settings.Default.UserFolderPath;

            //if (parent_id != "913989033028")
            //{
            //    Model.Dir parent_dir = Model.Dir.FindById(parent_id);
            //}

            Directory.CreateDirectory(path + "\\" + name);
            return;
        }

        public static void RemoveOnClient(string id)
        {
            Model.Dir dir = null;//Model.Dir.FindById(id);
            Directory.Delete(dir.Path, true);
            dir.Remove();
        }


        public static long getParentDirId(String path)
        {
            path = path.Substring(Properties.Settings.Default.UserFolderPath.Length + 1);
            string[] nestedFolders = path.Split('\\');
            long parent_id = 913989033028;

            if (nestedFolders.Length != 1)
            {
                Model.Dir current_dir;
                for (int i = 0; i < nestedFolders.Length-1; ++i)
                {
                    current_dir = Model.Dir.FindByNameAndParentId(nestedFolders[i], parent_id, false);
                    parent_id = current_dir.Id;
                }
            }
            return parent_id;
        }

        //получаем список удаленных папок
        public static List<Model.Dir> syncDirsWithDb()
        {
            return null;
        }

        public static List<string> getNewOfflineDirs()
        {
            /*
            List<string> result = new List<string>();
            List<Model.Dir> dirs = Model.Dir.All();

            foreach (var one in dirs)
            {
                if (!Directory.Exists(one.Path))
                {
                    //result.Add(one);
                    //Directory.CreateDirectory(path);
                }
            }*/

            return null;
        }

        public static void eventHendler(object sender, FileSystemEventArgs e)
        {
            int i;
            string[] dirs;
            Queue<string> queue = new Queue<string>();
            
            switch (e.ChangeType.ToString())
            {
                case "Created":
                    queue.Enqueue(e.FullPath);
                    do
                    {
                        //Controller.ApiTwoSafe.makeDir(Controller.Dirs.getParentDirId(queue.Peek()), Path.GetFileName(queue.Peek()), null);
                        dirs = Directory.GetDirectories(queue.Peek());
                        for (i = 0; i < dirs.Length; ++i)
                        {
                            queue.Enqueue(dirs[i]);
                        }
                        queue.Dequeue();
                    }
                    while (queue.Count != 0);
                    break;

                case "Deleted":
                    Model.Dir dir = Model.Dir.FindByPath(e.FullPath);
                    Controller.ApiTwoSafe.removeDir(dir.Id.ToString(), null, false);
                    dir.Remove();
                    break;
                default:
                    break;
            }
        }

    }
}
