using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;

namespace TwoSafe.Controller
{
    class Dirs : Db
    {
        /// <summary>
        /// Добавляет папку в пазу данных
        /// </summary>
        /// <param name="id">ID папки на сайте 2safe</param>
        /// <param name="parent_id">parent_id папки на сайте 2safe</param>
        /// <param name="name">Название папки</param>
        /// <returns>Возвращает TRUE, если операция прошла успешно</returns>
        public static bool add(Model.Dir dir)
        {
            bool returnCode = true;
            string values = "'" + dir.Id + "', '" + dir.Parent_id + "', '" + dir.Name + "'";

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

        /// <summary>
        /// Находит папку в базе данных по ID
        /// </summary>
        /// <param name="id">ID папки на сайте 2safe</param>
        /// <returns>Возвращает объект папки</returns>
        public static Model.Dir findById(string id)
        {
            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs WHERE id='" + id + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            Model.Dir dir = new Model.Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));

            reader.Close();
            connection.Close();

            return dir;
        }

        public static string getParentDirId(String path)
        {
            path = path.Substring(Properties.Settings.Default.UserFolderPath.Length + 1);
            string[] nestedFolders = path.Split('\\');
            string parent_id = "913989033028";

            if (nestedFolders.Length != 1)
            {
                Model.Dir current_dir;
                for (int i = 0; i < nestedFolders.Length-1; ++i)
                {
                    current_dir = findByNameAndParentId(nestedFolders[i], parent_id);
                    parent_id = current_dir.Id.ToString();
                }
            }
            return parent_id;
        }


        public static Model.Dir findByNameAndParentId(string name, string parent_id)
        {
            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs WHERE name='" + name + "' AND parent_id='" + parent_id + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            Model.Dir dir = new Model.Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));

            reader.Close();
            connection.Close();
            return dir;
        }

    }
}
