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
        
        public static void start()
        {
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.listDir("", Model.User.token);
            if (json.ContainsKey("error_code"))
            {
                return;
            }
        }

        public static void doSync()
        {
            for (int i = 0; i < queue.Count; ++i)
            {
                string[] item = (string[])queue.Dequeue();
                Dictionary<string, string> postData;

                switch (item[0])
                {
                    case "changed":
                        postData = new Dictionary<string, string>();
                        postData.Add("overwrite", "true");
                        Controller.ApiTwoSafe.putFile("913989033028", item[1], postData);
                        break;
                    case "created":
                        postData = new Dictionary<string, string>();
                        postData.Add("overwrite", "true");
                        Dictionary<string, dynamic> json = Controller.ApiTwoSafe.putFile("913989033028", item[1], postData);
                        if (!json.ContainsKey("error_code"))
                        {
                            Controller.Files.add(json["response"]["file"]["id"], "913989033028", json["response"]["file"]["name"]);
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

        public static void dirEvents(object sender, FileSystemEventArgs e)
        {
            int i;
            string[] dirs;
            Queue<string> queue = new Queue<string>();
            queue.Enqueue(e.FullPath);

            do
            {
                Controller.ApiTwoSafe.makeDir(Controller.Dirs.getParentDirId(queue.Peek()), Path.GetFileName(queue.Peek()), null);
                dirs = Directory.GetDirectories(queue.Peek());
                for (i = 0; i < dirs.Length; ++i)
                {
                    queue.Enqueue(dirs[i]);
                }
                queue.Dequeue();
            }
            while (queue.Count != 0);
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
                json = Controller.ApiTwoSafe.listDir(stek[0].ToString(), Model.User.token);

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
                        Controller.Dirs.add(new Model.Dir(json["response"]["list_dirs"][i]["id"], current_id, json["response"]["list_dirs"][i]["name"]));
                        stek.Add(json["response"]["list_dirs"][i]["id"]);
                    }



                    files = json["response"]["list_files"];
                    for (int i = 0; i < files.Count; ++i)
                    {
                        Controller.Files.add(json["response"]["list_files"][i]["id"], current_id, json["response"]["list_files"][i]["name"]);
                    }
                }

                stek.RemoveAt(0);
            }
        }
    }
}
