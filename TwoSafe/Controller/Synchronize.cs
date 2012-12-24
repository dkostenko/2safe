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
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.listDir("", Properties.Settings.Default.Token);
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
                    postData.Add("token", Properties.Settings.Default.Token);
                    postData.Add("overwrite", "true");
                    Controller.ApiTwoSafe.putFile(postData, arr[1]);
                }
                if (arr[0] == "created")
                {
                    Dictionary<string, string> postData = new Dictionary<string, string>();
                    postData.Add("dir_id", "2285033047");
                    postData.Add("token", Properties.Settings.Default.Token);
                    Controller.ApiTwoSafe.putFile(postData, arr[1]);
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

    }
}
