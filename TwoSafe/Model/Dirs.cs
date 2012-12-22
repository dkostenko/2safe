using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class Dirs : Db
    {
        public static bool add(string id, string parent_id, string name)
        {
            bool returnCode = true;
            string values = "'" + id + "', '" + parent_id + "', '" + name + "'"; ;

            try
            {
                executeNonQuery("insert into dirs(id, parent_id, name) values(" + values + ");");
            }
            catch (Exception fail)
            {
                returnCode = false;
            }
            return returnCode;
        }


    }
}
