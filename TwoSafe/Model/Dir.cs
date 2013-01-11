using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class Dir : ActiveRecord
    {
        private string _name;
        private long _id, _parent_id;
        private string _path;

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
                this.SetPath();
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
        /// Устанавливает полный путь до папки в синхронизируемой папке
        /// </summary>
        private void SetPath()
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

            this._path = Properties.Settings.Default.UserFolderPath + result;
        }

        /// <summary>
        /// Устанавливает полный путь до папки в синхронизируемой папке
        /// </summary>
        /// <param name="path">Полный путь до папки в синхронизируемой папке</param>
        private void SetPath(string path)
        {
            this._path = path;
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
                dir.SetPath();

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
        public static Dir FindByNameAndParentId(string name, string parent_id, bool setPath)
        {
            Dir result = null;
            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs WHERE name='" + name + "' AND parent_id='" + parent_id + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            try
            {
                reader.Read();
                result = new Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));
                if (setPath)
                {
                    result.SetPath();
                }
            }
            catch
            {
            }

            reader.Close();
            connection.Close();
            return result;
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
                dir = FindByNameAndParentId(parts[0], Properties.Settings.Default.RootId, false);
                for (int i = 1; i < parts.Length - 1; ++i)
                {
                    dir = FindByNameAndParentId(parts[i], dir.Id.ToString(), false);
                }
            }
            else
            {
                dir = FindByNameAndParentId(parts[0], Properties.Settings.Default.RootId, false);
            }
            dir.SetPath(path);
            return dir;
        }

        /// <summary>
        /// Возвращает все папки из базы данных
        /// </summary>
        /// <returns>Возвращает список папок: упорядоченных по возрастанию level.</returns>
        public static List<Dir> All()
        {
            List<Dir> dirs = new List<Dir>();
            Dir temp;

            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs", connection); // ORDER BY parent_id ASC
            SQLiteDataReader reader = command.ExecuteReader();


            while (reader.Read())
            {
                temp = new Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));
                temp.SetPath();
                dirs.Add(temp);
            }

            reader.Close();
            connection.Close();

            return dirs;
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

        public string Path
        {
            get { return _path; }
        }
    }
}
