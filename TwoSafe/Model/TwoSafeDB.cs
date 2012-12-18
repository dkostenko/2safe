using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class TwoSafeDB
    {
        public static void createTable()
        {
            SQLiteConnection.CreateFile("twoSafe.db");

            SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=twoSafe.db;Version=3;");

            m_dbConnection.Open(); 
            string sql = "CREATE TABLE folders (id INTEGER, parent_id INT, title TEXT, old_title TEXT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection); 
            command.ExecuteNonQuery();

            sql = "insert into folders (title, old_title, id, parent_id) values ('Myself', '', 12123123123, 234234234)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            
            m_dbConnection.Close();
        }
    }
}
