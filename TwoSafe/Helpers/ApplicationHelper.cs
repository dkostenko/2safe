using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Helpers
{
    class ApplicationHelper
    {
        public static void SetCurrentTimeToSettings()
        {
            TimeSpan span = (DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0));
            Properties.Settings.Default.LastGetEventsTime = (uint)span.TotalSeconds;
            Properties.Settings.Default.Save();
        }

    }
}
