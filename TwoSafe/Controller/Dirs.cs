using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TwoSafe.Controller
{
    class Dirs
    {
        /// <summary>
        /// Обработчик события создания папки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Create(object sender, FileSystemEventArgs e)
        {
            string[] dirs;
            Queue<string> queue = new Queue<string>();

            Model.Dir parent_dir = Model.Dir.FindParentByPath(e.Name);
            if(parent_dir == null) Model.Dir.Upload(Properties.Settings.Default.RootId, e.FullPath);
            else Model.Dir.Upload(parent_dir.Id, e.FullPath);


            Helpers.ApplicationHelper.SetCurrentTimeToSettings();
        }

        /// <summary>
        /// Обработчик события удаления папки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Delete(object sender, FileSystemEventArgs e)
        {
            Model.Dir dir = Model.Dir.FindByPath(e.FullPath);
            dir.RemoveOnServer();

            Helpers.ApplicationHelper.SetCurrentTimeToSettings();
        }

        /// <summary>
        /// Обработчик события переименования папки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Rename(object sender, RenamedEventArgs e)
        {
            int i = 0;
            i++;
        }
    }
}
