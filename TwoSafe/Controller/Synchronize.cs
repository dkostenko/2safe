using System;
using System.Data;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using System.Threading;

namespace TwoSafe.Controller
{
    class Synchronize
    {
        private static FileSystemWatcher dirWatcher = new FileSystemWatcher();
        private static FileSystemWatcher fileWatcher = new FileSystemWatcher();

        static Synchronize()
        {
            dirWatcher.Path = Properties.Settings.Default.UserFolderPath;
            dirWatcher.IncludeSubdirectories = true;
            dirWatcher.NotifyFilter = NotifyFilters.DirectoryName;
            dirWatcher.Created += new FileSystemEventHandler(Controller.Dirs.Create);
            dirWatcher.Deleted += new FileSystemEventHandler(Controller.Dirs.Delete);
            //dirWatcher.Changed += new FileSystemEventHandler(Controller.Dirs.Change);
            dirWatcher.Renamed += new RenamedEventHandler(Controller.Dirs.Rename);

            fileWatcher.Path = Properties.Settings.Default.UserFolderPath;
            fileWatcher.IncludeSubdirectories = true;
            fileWatcher.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite | NotifyFilters.FileName;
            fileWatcher.Created += new FileSystemEventHandler(Controller.Files.Create);
            //fileWatcher.Changed += new FileSystemEventHandler(Controller.Synchronize.eventRaised);
            fileWatcher.Deleted += new FileSystemEventHandler(Controller.Files.Delete);
            fileWatcher.Renamed += new RenamedEventHandler(Controller.Files.Rename);
        }

        /// <summary>
        /// Начинает следить за изменениями в локальной папке
        /// </summary>
        public static void MonitorChanges()
        {
            dirWatcher.EnableRaisingEvents = true;
            fileWatcher.EnableRaisingEvents = true;
        }

        /// <summary>
        /// Прекращает следить за изменениями в локальной папке
        /// </summary>
        public static void DoNotMonitorChanges()
        {
            dirWatcher.EnableRaisingEvents = false;
            fileWatcher.EnableRaisingEvents = false;
        }

        /// <summary>
        /// Полная синхронизация локальной папки с сервером при включении программы или при одиночном запросе на изменения
        /// </summary>
        public static void Start()
        {
            DoNotMonitorChanges();
            int i, j;
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.getEvents();
            System.Collections.ArrayList tempArray;
            Model.Dir dir;
            Model.File file;
            string[] subDirs, filesInDir;
            List<Model.Dir> que = new List<Model.Dir>();
            List<Model.Dir> ImmuneDirs = new List<Model.Dir>();


            List<Model.File> s_file_add = new List<Model.File>();
            List<Model.File> s_file_del = new List<Model.File>();
            List<Model.File> s_file_ren = new List<Model.File>();
            List<Model.File> s_file_mov = new List<Model.File>();
            List<Model.Dir> s_dir_add = new List<Model.Dir>();
            List<Model.Dir> s_dir_del = new List<Model.Dir>();
            List<Model.Dir> s_dir_ren = new List<Model.Dir>();
            List<Model.Dir> s_dir_mov = new List<Model.Dir>();

            List<Model.File> LD_file_add = new List<Model.File>();
            List<Model.Dir> LD_dir_add = new List<Model.Dir>();
            List<Model.Dir> LD_dir_del = new List<Model.Dir>();
            List<Model.File> LD_file_del = new List<Model.File>();

            foreach (var one in json["response"]["events"])
            {
                if (one["event"] == "dir_created")
                {
                    s_dir_add.Add(new Model.Dir(one["id"], one["parent_id"], one["name"]));
                    continue;
                }
                if (one["event"] == "dir_moved")
                {
                    if (one["new_parent_id"] == Properties.Settings.Default.TrashId.ToString())
                    {
                        s_dir_del.Add(new Model.Dir(one["id"], one["old_parent_id"], one["old_name"]));
                        continue;
                    }
                    if (one["old_parent_id"] == one["new_parent_id"])
                    {
                        s_dir_ren.Add(new Model.Dir(one["id"], one["old_parent_id"], one["new_name"], one["old_name"], "0"));
                        continue;
                    }
                    s_dir_mov.Add(new Model.Dir(one["id"], one["new_parent_id"], one["old_name"], null, one["old_parent_id"]));
                    continue;
                }
                if (one["event"] == "file_uploaded" && one.ContainsKey("size"))
                {
                    s_file_add.Add(new Model.File(one["id"], one["parent_id"], one["name"]));
                    continue;
                }
                if (one["event"] == "file_moved")
                {
                    if (one["new_parent_id"] == Properties.Settings.Default.TrashId.ToString())
                    {
                        //удалить файл
                        s_file_del.Add(new Model.File(one["new_id"], one["old_parent_id"], one["new_name"], one["old_id"], one["old_parent_id"], one["old_name"], "0"));
                        continue;
                    }
                    if (one["old_parent_id"] == one["new_parent_id"])
                    {
                        //переименовать файл
                        s_file_ren.Add(new Model.File(one["new_id"], one["new_parent_id"], one["new_name"], one["old_id"], one["old_parent_id"], one["old_name"], one["version_id"]));
                        continue;
                    }
                    //переместить файл
                    s_file_mov.Add(new Model.File(one["new_id"], one["new_parent_id"], one["new_name"], one["old_id"], one["old_parent_id"], one["old_name"], one["version_id"]));
                    continue;
                }
            }

            //создаем чистый список добавлений файлов на сервере
            //Добавляем каждую папку и родительские папки в ImmuneDirs
            for (i = 0; i < s_file_add.Count; ++i)
            {
                json = Controller.ApiTwoSafe.getProps(s_file_add[i].Id)["response"]["object"];
                if (!json.ContainsKey("is_trash"))
                {
                    if (json["dir_id"] == Properties.Settings.Default.RootId.ToString())
                    {
                        ImmuneDirs.Add(new Model.Dir(json["id"], json["dir_id"], json["name"]));
                    }
                    else
                    {
                        tempArray = Controller.ApiTwoSafe.getTreeParent(json["dir_id"])["response"]["tree"];
                        for (j = 0; j < tempArray.Count - 1; ++j)
                        {
                            json = (Dictionary<string, dynamic>)tempArray[j];
                            ImmuneDirs.Add(new Model.Dir(json["id"], json["parent_id"], json["name"]));
                        }
                    }
                }
                else
                {
                    for (j = 0; j < s_file_del.Count; ++j)
                    {
                        if (s_file_add[i].Id == s_file_del[j].Id)
                        {
                            s_file_del.RemoveAt(j);
                        }
                    }
                    s_file_add.RemoveAt(i);
                }
            }


            //создаем чистый список добавлений папок на сервере
            //Добавляем каждую папку и родительские папки в ImmuneDirs
            for (i = 0; i < s_dir_add.Count; ++i)
            {
                json = Controller.ApiTwoSafe.getProps(s_dir_add[i].Id)["response"]["object"];
                if (!json.ContainsKey("is_trash"))
                {
                    if (json["parent_id"] == Properties.Settings.Default.RootId.ToString())
                    {
                        ImmuneDirs.Add(new Model.Dir(json["id"], json["parent_id"], json["name"]));
                    }
                    else
                    {
                        tempArray = Controller.ApiTwoSafe.getTreeParent(json["parent_id"])["response"]["tree"];
                        for (j = 0; j < tempArray.Count - 1; ++j)
                        {
                            json = (Dictionary<string, dynamic>)tempArray[j];
                            ImmuneDirs.Add(new Model.Dir(json["id"], json["parent_id"], json["name"]));
                        }
                    }
                }
                else
                {
                    s_dir_add.RemoveAt(i);
                }
            }


            //создаем чистый список удалений папок на сервере
            //Удаляем каждую папку из s_dir_del, которая содержится в ImmuneDirs
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
            foreach (var one in s_dir_add)
            {
                one.Download();
            }
            foreach (var one in s_file_add)
            {
                one.Download();
            }




            //3) получить список изменений в LD
            LD_dir_del.InsertRange(0, Model.Dir.All(Properties.Settings.Default.RootId));
            subDirs = Directory.GetDirectories(Properties.Settings.Default.UserFolderPath);
            foreach (string path in subDirs)
            {
                dir = Model.Dir.FindByNameAndParentId(Path.GetFileName(path), Properties.Settings.Default.RootId);
                if (dir == null)
                {
                    dir = Model.Dir.Upload(Properties.Settings.Default.RootId, path);
                }
                else
                {
                    foreach (Model.Dir one in LD_dir_del)
                    {
                        if (one.Id == dir.Id)
                        {
                            LD_dir_del.Remove(one);
                            break;
                        }
                    }
                }
                que.Add(dir);
            }

            filesInDir = Directory.GetFiles(Properties.Settings.Default.UserFolderPath);
            for (i = 0; i < filesInDir.Length; ++i)
            {
                if (Model.File.FindByNameAndParentId(Path.GetFileName(filesInDir[i]), Properties.Settings.Default.RootId) == null)
                {
                    Model.File.Upload(Properties.Settings.Default.RootId, filesInDir[i]);
                }
            }



            while (que.Count > 0)
            {
                if (Directory.Exists(que[0].Path))
                {
                    subDirs = Directory.GetDirectories(que[0].Path);
                    LD_dir_del.InsertRange(0, Model.Dir.All(que[0].Id));
                    for (i = 0; i < subDirs.Length; ++i)
                    {
                        dir = Model.Dir.FindByNameAndParentId(Path.GetFileName(subDirs[i]), que[0].Id);
                        if (dir == null)
                        {
                            dir = Model.Dir.Upload(que[0].Id, subDirs[i]);
                            LD_dir_add.Add(dir);
                        }
                        else
                        {
                            foreach (Model.Dir one in LD_dir_del)
                            {
                                if (one.Id == dir.Id)
                                {
                                    LD_dir_del.Remove(one);
                                    break;
                                }
                            }
                        }
                        que.Add(dir);
                    }


                    filesInDir = Directory.GetFiles(que[0].Path);
                    for (i = 0; i < filesInDir.Length; ++i)
                    {
                        file = Model.File.FindByNameAndParentId(Path.GetFileName(filesInDir[i]), que[0].Id);
                        if (file == null)
                        {
                            Model.File.Upload(que[0].Id, filesInDir[i]);
                        }
                    }
                }
                que.RemoveAt(0);
            }

            //4) применим чистый список переименований на сервере
            foreach (var one in s_dir_ren)
            {
                one.RenameOnClient();
            }

            foreach (var one in s_file_ren)
            {
                one.RenameOnClient();
            }

            //5) применим чистый список перемещений на сервере
            foreach (var one in s_dir_mov)
            {
                one.MoveOnClient();
            }

            foreach (var one in s_file_mov)
            {
                one.MoveOnClient();
            }

            //6) применим чистый список удалений на сервере и в папке
            foreach (var one in LD_dir_del)
            {
                one.RemoveOnServer();
            }
            foreach (var one in s_dir_del)
            {
                one.RemoveOnClient();
            }
            foreach (var one in s_file_del)
            {
                one.RemoveOnClient();
            }

            Helpers.ApplicationHelper.SetCurrentTimeToSettings();
            MonitorChanges();
        }
        
        /// <summary>
        /// Полностью клонирует сервер в локальную папку
        /// </summary>
        public static void CloneServer()
        {
            Dictionary<string, dynamic> json;
            Model.Dir dir, current_dir;

            Stack<Model.Dir> stack = new Stack<Model.Dir>();

            //просматриваем корневую папку сервера и помещаем папки, содержащиеся в ней, в стек
            json = Controller.ApiTwoSafe.listDir("");

            foreach(var one in json["response"]["list_dirs"])
            {
                if (one["name"] != "Shared" && one["name"] != "Trash")
                {
                    dir = new Model.Dir(one["id"], Properties.Settings.Default.RootId, one["name"]);
                    dir.Download();
                    stack.Push(dir);
                }
            }

            foreach (var one in json["response"]["list_files"])
            {
                new Model.File(one["id"], Properties.Settings.Default.RootId, one["name"]).Download();
            }

            //просматриваем все остальные папки с сервера и помещаем папки, содержащиеся в ней, в стек
            while (stack.Count != 0)
            {
                current_dir = stack.Pop();

                json = Controller.ApiTwoSafe.listDir(current_dir.Id.ToString());

                foreach (var one in json["response"]["list_dirs"])
                {
                    dir = new Model.Dir(one["id"], current_dir.Id, one["name"]);
                    dir.Download();
                    stack.Push(dir);
                }

                foreach (var one in json["response"]["list_files"])
                {
                    new Model.File(one["id"], current_dir.Id, one["name"]).Download();
                }
            }


            Helpers.ApplicationHelper.SetCurrentTimeToSettings();

            Start();
        }
    }
}
