using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace TwoSafe.Controller
{
    class Dirs
    {

        public static void createOnClient(string where, string name)
        {
            Model.Dir dir = new Model.Dir("", "", "");
            dir.Save();
            System.IO.Directory.CreateDirectory(where);
            return;
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
