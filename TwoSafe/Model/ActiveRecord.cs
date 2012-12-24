using System;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class ActiveRecord
    {
        protected static string dbName;

        static ActiveRecord()
        {
            dbName = "Data Source=twoSafe.db;Version=3;";
        }


        protected static int ExecuteNonQuery(string sql)
        {
            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand mycommand = new SQLiteCommand(connection);
            mycommand.CommandText = sql;
            int rowsUpdated = mycommand.ExecuteNonQuery();
            connection.Close();
            return rowsUpdated;
        }
    }
}
