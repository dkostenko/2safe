using System;
using System.Collections.Generic;
using System.Collections;
using System.Data;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class Db
    {
        protected static string dbName;

        static Db()
        {
            dbName = "Data Source=twoSafe.db;Version=3;";
        }


        //создание БД УДАЛИТЬ
        public static void create()
        {
            SQLiteConnection.CreateFile("twoSafe.db");

            SQLiteConnection m_dbConnection = new SQLiteConnection(dbName);

            m_dbConnection.Open();
            string sql = "CREATE TABLE dirs (id INTEGER, parent_id INTEGER, name TEXT)";
            SQLiteCommand command = new SQLiteCommand(sql, m_dbConnection); 
            command.ExecuteNonQuery();

            sql = "CREATE TABLE files (id INTEGER, parent_id INTEGER, name TEXT, version_id INTEGER, chksum TEXT, size INTEGER)";
            command = new SQLiteCommand(sql, m_dbConnection);
            command.ExecuteNonQuery();
            
            m_dbConnection.Close();
        }

        // очистка таблиц УДАЛИТЬ
        public static bool clearTables()
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection(dbName);
            m_dbConnection.Open();
            try
            {
                ExecuteNonQuery("delete from dirs;");
                ExecuteNonQuery("delete from files;");
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                m_dbConnection.Close();
            }
        }


        protected static int ExecuteNonQuery(string sql)
        {
            SQLiteConnection cnn = new SQLiteConnection(dbName);
            cnn.Open();
            SQLiteCommand mycommand = new SQLiteCommand(cnn);
            mycommand.CommandText = sql;
            int rowsUpdated = mycommand.ExecuteNonQuery();
            cnn.Close();
            return rowsUpdated;
        }
    }
}
