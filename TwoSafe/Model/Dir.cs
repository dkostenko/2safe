using System;
using System.Collections;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class Dir : ActiveRecord
    {
        private string _name;
        private long _id, _parent_id;

        public Dir(long id, long parent_id, string name)
        {
            this._id = id;
            this._parent_id = parent_id;
            this._name = name;
        }

        public Dir(string id, string parent_id, string name)
        {
            this._id = long.Parse(id);
            this._parent_id = long.Parse(parent_id);
            this._name = name;
        }

        /// <summary>
        /// Сохраняет папку в базу данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно, иначе - FALSE</returns>
        public bool Save()
        {
            bool result = true;
            string values = "'" + this.Id + "', '" + this.Parent_id + "', '" + this.Name + "'";

            try
            {
                ExecuteNonQuery("INSERT into dirs(id, parent_id, name) values(" + values + ");");
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Удаляет папку из базы данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно, иначе - FALSE</returns>
        public bool Remove()
        {
            bool result = true;
            try
            {
                ExecuteNonQuery("DELETE from dirs where id='" + this.Id + "'");
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Находит полный путь до папки в синхронизируемой папке
        /// </summary>
        public string GetPath()
        {
            string result = "";
            ArrayList path = new ArrayList();

            Dir parentDir = this;

            while (parentDir != null)
            {
                path.Add(parentDir.Name);
                parentDir = FindById(parentDir.Parent_id.ToString());
                if (parentDir == null) break;
            }

            for (int i = path.Count - 1; i >= 0; --i)
            {
                result += "\\" + path[i].ToString();
            }

            return Properties.Settings.Default.UserFolderPath + result;
        }

        /// <summary>
        /// Находит папку в базе данных по ID
        /// </summary>
        /// <param name="id">ID папки на сайте 2safe</param>
        /// <returns>Возвращает объект папки</returns>
        public static Dir FindById(string id)
        {
            Model.Dir dir = null;
            if (id != "913989033028")
            {
                SQLiteConnection connection = new SQLiteConnection(dbName);
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs WHERE id='" + id + "'", connection);
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();

                dir = new Model.Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));

                reader.Close();
                connection.Close();
            }

            return dir;
        }

        /// <summary>
        /// Находит папку в базе данных по названию и ID родительской папки
        /// </summary>
        /// <param name="name">Имя папки</param>
        /// <param name="parent_id">ID родительской папки</param>
        /// <returns>Возвращает объект папки</returns>
        public static Dir FindByNameAndParentId(string name, string parent_id)
        {
            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs WHERE name='" + name + "' AND parent_id='" + parent_id + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            Dir dir = new Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));

            reader.Close();
            connection.Close();
            return dir;
        }

        /// <summary>
        /// Находит папку в базе данных по пути к папке
        /// </summary>
        /// <param name="path">Путь к папке</param>
        /// <returns>Возвращает объект папки или NULL, если папка не найдена или эта папка - корень.</returns>
        public static Dir FindByPath(String path)
        {
            Dir dir = null;
            string[] parts = path.Substring(Properties.Settings.Default.UserFolderPath.Length + 1).Split('\\');
            if (parts.Length != 1)
            {
                dir = FindByNameAndParentId(parts[0], Properties.Settings.Default.RootId);
                for (int i = 1; i < parts.Length - 1; ++i)
                {
                    dir = FindByNameAndParentId(parts[i], dir.Id.ToString());
                }
            }
            return dir;
        }

        public long Id
        {
            get { return _id; }
        }

        public long Parent_id
        {
            get { return _parent_id; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
