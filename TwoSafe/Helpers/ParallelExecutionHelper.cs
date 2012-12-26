using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Helpers
{
    /// <summary>
    /// Класс для помощи исполниния параллельного кода
    /// </summary>
    public class ParallelExecutionHelper
    {
        public event EventHandler LogInFinished;

        protected virtual void OnLoginIsFinished(EventArgs e)
        {
            if (LogInFinished != null)
            {
                LogInFinished(this, e);
            }
        }
    }
}
