using System.IO;
namespace TwoSafe.Controller
{
    class Files
    {
        /// <summary>
        /// Обработчик события создания файла в локальной папке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Create(object sender, FileSystemEventArgs e)
        {
            Model.Dir parent_dir = Model.Dir.FindParentByPath(e.FullPath);

            if (parent_dir == null) Model.File.Upload(Properties.Settings.Default.RootId, e.FullPath);
            else Model.File.Upload(parent_dir.Id, e.FullPath);

            Helpers.ApplicationHelper.SetCurrentTimeToSettings();
        }

        /// <summary>
        /// Обработчик события удаления файла из локальной папки
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Delete(object sender, FileSystemEventArgs e)
        {
            Model.File file = Model.File.FindByPath(e.FullPath);
            file.RemoveOnServer();

            Helpers.ApplicationHelper.SetCurrentTimeToSettings();
        }

        /// <summary>
        /// Обработчик события переименования файла в локальной папке
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public static void Rename(object sender, RenamedEventArgs e)
        {
            Model.File file = Model.File.FindByPath(e.OldFullPath);
            file.RenameOnServer(e.FullPath);

            Helpers.ApplicationHelper.SetCurrentTimeToSettings();
        }
    }
}
