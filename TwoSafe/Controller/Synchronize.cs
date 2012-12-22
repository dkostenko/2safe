using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

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
                string qwe = (string)queue.Dequeue();
                string[] arr = qwe.Split('#');
                if (arr[0] == "changed")
                {
                    Dictionary<string, string> postData = new Dictionary<string, string>();
                    postData.Add("dir_id", "2285033047");
                    postData.Add("token", Model.User.token);
                    postData.Add("overwrite", "true");
                    //Controller.ApiTwoSafe.putFile(postData, arr[1]);
                }
                if (arr[0] == "created")
                {
                    Dictionary<string, string> postData = new Dictionary<string, string>();
                    postData.Add("dir_id", "2285033047");
                    postData.Add("token", Model.User.token);
                    //Controller.ApiTwoSafe.putFile(postData, arr[1]);
                }
                if (arr[0] == "deleted")
                {
                }
            }
        }

        public static void eventRaised(object sender, System.IO.FileSystemEventArgs e)
        {
            switch (e.ChangeType)
            {
                case WatcherChangeTypes.Changed:
                    queue.Enqueue("changed#" + e.FullPath);
                    break;
                case WatcherChangeTypes.Created:
                    queue.Enqueue("created#" + e.FullPath);
                    break;
                case WatcherChangeTypes.Deleted:
                    queue.Enqueue("deleted#" + e.FullPath);
                    break;
                default: // Another action
                    break;
            }
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
                    for (int i = 0; i < dirs.Count; ++i)
                    {
                        Model.Dirs.add(json["response"]["list_dirs"][i]["id"], current_id, json["response"]["list_dirs"][i]["name"]);
                        stek.Add(json["response"]["list_dirs"][i]["id"]);
                    }

                    files = json["response"]["list_files"];
                    for (int i = 0; i < files.Count; ++i)
                    {
                        Model.Files.add(json["response"]["list_files"][i]["id"], current_id, json["response"]["list_files"][i]["name"]);
                    }
                }

                stek.RemoveAt(0);
            }
        }
    }
}
