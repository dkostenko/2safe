using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Model
{
    class Files : Db
    {
        public static bool add(string id, string parent_id, string name)
        {
            bool returnCode = true;
            string values = "'" + id + "', '" + parent_id + "', '" + name + "'"; ;

            try
            {
                executeNonQuery("insert into files(id, parent_id, name) values(" + values + ");");
            }
            catch (Exception fail)
            {
                returnCode = false;
            }
            return returnCode;
        }
    }
}
