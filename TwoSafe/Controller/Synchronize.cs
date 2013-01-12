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
        private static Queue queue;

        static Synchronize()
        {
            queue = new Queue();
        }


        public static void fromServerToClient()
        {
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.getEvents();
            foreach (var one in json["response"]["events"])
            {
                if (one["event"] == "dir_created")
                {
                    Controller.Dirs.CreateOnClient(one["id"], one["parent_id"], one["name"]);
                }
                if (one["event"] == "dir_moved")
                {
                    Controller.Dirs.RemoveOnClient(one["id"]);
                }
                if (one["event"] == "file_moved")
                {
                    Controller.Files.RemoveOnClient(one["old_name"], one["old_parent_id"]);
                }
            }
        }

        public static void doSync()
        {
            Dictionary<string, dynamic> json = null;
            for (int i = 0; i < queue.Count; ++i)
            {
                string[] item = (string[])queue.Dequeue();
                Dictionary<string, string> postData;

                switch (item[0])
                {
                    case "changed":
                        postData = new Dictionary<string, string>();
                        postData.Add("overwrite", "true");
                        //Controller.ApiTwoSafe.putFile(Properties.Settings.Default.RootId, item[1], postData);
                        break;
                    case "created":
                        postData = new Dictionary<string, string>();
                        postData.Add("overwrite", "true");

                        Model.Dir dir = Model.Dir.FindByPath(item[1]);
                        if (dir == null)
                        {
                            //json = Controller.ApiTwoSafe.putFile(Properties.Settings.Default.RootId, item[1], postData);
                        }
                        else
                        {
                            //json = Controller.ApiTwoSafe.putFile(dir.Id.ToString(), item[1], postData);
                        }
                        
                        if (!json.ContainsKey("error_code"))
                        {
                            //new Model.File(json["response"]["file"]["id"], "913989033028", json["response"]["file"]["name"], json["response"]["file"]["version_id"], json["response"]["file"]["chksum"], json["response"]["file"]["size"]).Save();
                        }
                        break;
                    case "deleted":
                        break;
                    default:
                        break;
                }
            }
        }



        public static void eventRaised(object sender, FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    queue.Enqueue(new string[] { "changed", e.FullPath });
                    break;
                case WatcherChangeTypes.Created:
                    queue.Enqueue(new string[] { "created", e.FullPath });
                    break;
                case WatcherChangeTypes.Deleted:
                    queue.Enqueue(new string[] { "deleted", e.FullPath });
                    break;
                default: // Another action
                    break;
            }
            doSync();
        }


        public static void Start()
        {
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
            List<Model.Dir> s_dir_add = new List<Model.Dir>();
            List<Model.Dir> s_dir_del = new List<Model.Dir>();

            List<Model.File> LD_file_add = new List<Model.File>();
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
                if (one["event"] == "file_uploaded" && one.ContainsKey("size"))
                {
                    s_file_add.Add(new Model.File(one["id"], one["parent_id"], one["name"]));
                    continue;
                }
                if (one["event"] == "file_moved" && one["new_parent_id"] == "913990033028")
                {
                    //s_file_del.Add(new Model.File(one["id"], one["new_parent_id"], one["new_name"], "0", "0", 0));
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
            foreach (var one in s_dir_add)
            {
                one.Download();
            }
            foreach (var one in s_file_add)
            {
                one.Download();
            }




            //3) получить список изменений в LD

            subDirs = Directory.GetDirectories(Properties.Settings.Default.UserFolderPath);
            for (i = 0; i < subDirs.Length; ++i)
            {
                dir = Model.Dir.FindByNameAndParentId(Path.GetFileName(subDirs[i]), Properties.Settings.Default.RootId, true);
                if (dir == null)
                {
                    Model.Dir.Upload(Properties.Settings.Default.RootId, subDirs[i]);
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
                    for (i = 0; i < subDirs.Length; ++i)
                    {
                        dir = Model.Dir.FindByNameAndParentId(Path.GetFileName(subDirs[i]), que[0].Id, true);
                        if (dir == null)
                        {
                            dir = Model.Dir.Upload(que[0].Id, subDirs[i]);
                            LD_dir_add.Add(dir);
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
                else
                {
                    LD_dir_del.Add(que[0]);
                }

                que.Remove(que[0]);
            }

            Helpers.ApplicationHelper.SetCurrentTimeToSettings();
        }
        

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
        }
    }
}
