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
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.getEvents((Properties.Settings.Default.LastGetEventsTime).ToString());
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












        public static void start()
        {
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.listDir("");
            if (json.ContainsKey("error_code"))
            {
                return;
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
                        Controller.ApiTwoSafe.putFile(Properties.Settings.Default.RootId, item[1], postData);
                        break;
                    case "created":
                        postData = new Dictionary<string, string>();
                        postData.Add("overwrite", "true");

                        Model.Dir dir = Model.Dir.FindByPath(item[1]);
                        if (dir == null)
                        {
                            json = Controller.ApiTwoSafe.putFile(Properties.Settings.Default.RootId, item[1], postData);
                        }
                        else
                        {
                            json = Controller.ApiTwoSafe.putFile(dir.Id.ToString(), item[1], postData);
                        }
                        
                        if (!json.ContainsKey("error_code"))
                        {
                            new Model.File(json["response"]["file"]["id"], "913989033028", json["response"]["file"]["name"]).Save();
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



        public static void inception()
        {
            string current_id;
            Dictionary<string, dynamic> json = new Dictionary<string, dynamic>();
            ArrayList dirs, files, stek;

            stek = new System.Collections.ArrayList();
            stek.Add("");

            while (stek.Count != 0)
            {
                json = Controller.ApiTwoSafe.listDir(stek[0].ToString());

                if (json.ContainsKey("error_code"))
                {
                    //TODO: Обработать ошибку
                }
                else
                {
                    current_id = json["response"]["root"]["id"];
                    dirs = json["response"]["list_dirs"];

                    for (int i = 0; i < json["response"]["list_dirs"].Count; ++i)
                    {
                        new Model.Dir(json["response"]["list_dirs"][i]["id"], current_id, json["response"]["list_dirs"][i]["name"], 0);
                        stek.Add(json["response"]["list_dirs"][i]["id"]);
                    }



                    files = json["response"]["list_files"];
                    for (int i = 0; i < files.Count; ++i)
                    {
                        new Model.Dir(json["response"]["list_files"][i]["id"], current_id, json["response"]["list_files"][i]["name"], 0);
                    }
                }

                stek.RemoveAt(0);
            }
        }


    }
}
