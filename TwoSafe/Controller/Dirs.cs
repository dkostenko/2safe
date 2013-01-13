using System.Collections.Generic;
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
            if (Model.Dir.FindByPath(e.FullPath) == null)
            {
                Queue<string> queue = new Queue<string>();
                string[] dirs = Directory.GetDirectories(e.FullPath);
                string[] files = Directory.GetFiles(e.FullPath);
                foreach (string path in dirs)
                    queue.Enqueue(path);

                Model.Dir current_dir = Model.Dir.FindParentByPath(e.FullPath);

                current_dir = Model.Dir.Upload(current_dir.Id, e.FullPath);
                foreach (string one in files)
                    Model.File.Upload(current_dir.Id, one);


                while (queue.Count != 0)
                {
                    current_dir = Model.Dir.FindParentByPath(queue.Peek());
                    current_dir = Model.Dir.Upload(current_dir.Id, queue.Peek());

                    dirs = Directory.GetDirectories(queue.Peek());
                    files = Directory.GetFiles(queue.Dequeue());

                    foreach (string one in files)
                        Model.File.Upload(current_dir.Id, one);

                    foreach (string path in dirs)
                        queue.Enqueue(path);
                }

                Helpers.ApplicationHelper.SetCurrentTimeToSettings();
            }
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
            Model.Dir dir = Model.Dir.FindByPath(e.OldFullPath);
            dir.RenameOnServer(e.FullPath);

            Helpers.ApplicationHelper.SetCurrentTimeToSettings();
        }
    }
}
