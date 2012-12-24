using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;

namespace TwoSafe.Controller
{
    class Dirs
    {

        public static void CreateOnClient(string id, string parent_id, string name)
        {
            Model.Dir dir = new Model.Dir(id, parent_id, name);
            dir.Save();

            string path = Properties.Settings.Default.UserFolderPath;

            if (parent_id != "913989033028")
            {
                Model.Dir parent_dir = Model.Dir.FindById(parent_id);
                path = parent_dir.GetPath();
            }

            Directory.CreateDirectory(path + "\\" + name);
            return;
        }

        public static void RemoveOnClient(string id)
        {
            Model.Dir dir = Model.Dir.FindById(id);
            string path = dir.GetPath();
            Directory.Delete(path);
            dir.Remove();
        }


        public static string getParentDirId(String path)
        {
            path = path.Substring(Properties.Settings.Default.UserFolderPath.Length + 1);
            string[] nestedFolders = path.Split('\\');
            string parent_id = "913989033028";

            if (nestedFolders.Length != 1)
            {
                Model.Dir current_dir;
                for (int i = 0; i < nestedFolders.Length-1; ++i)
                {
                    current_dir = Model.Dir.FindByNameAndParentId(nestedFolders[i], parent_id);
                    parent_id = current_dir.Id.ToString();
                }
            }
            return parent_id;
        }



    }
}
