using System;
using System.Threading;

namespace TwoSafe.View
{
    static public class SingleInstance
    {
        private static Mutex mutex;
        public static Boolean restarting = false;

        static public bool Start()
        {
            bool onlyInstance = false;
            string mutexName = String.Format("Local\\{0}", ProgramInfo.AssemblyGuid);

            // if you want your app to be limited to a single instance
            // across ALL SESSIONS (multiple users & terminal services), then use the following line instead:
            // string mutexName = String.Format("Global\\{0}", ProgramInfo.AssemblyGuid);

            mutex = new Mutex(true, mutexName, out onlyInstance);
            return onlyInstance;
        }

        static public void Stop()
        {
            mutex.ReleaseMutex();
        }
    }
}
