using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TwoSafe.Model
{
    class Dir : ActiveRecord
    {
        private string _name, _path, _oldName;
        private long _id, _parent_id, _old_parent_id;

        public Dir(long id, long parent_id, string name)
        {
            this._id = id;
            this._parent_id = parent_id;
            this._name = name;
        }

        public Dir(string id, long parent_id, string name)
        {
            this._id = long.Parse(id);
            this._parent_id = parent_id;
            this._name = name;
        }

        public Dir(string id, string parent_id, string name)
        {
            this._id = long.Parse(id);
            this._parent_id = long.Parse(parent_id);
            this._name = name;
        }

        public Dir(string id, string parent_id, string name, string oldName, string oldParentId)
        {
            this._id = long.Parse(id);
            this._parent_id = long.Parse(parent_id);
            this._name = name;
            this._oldName = oldName;
            this._old_parent_id = long.Parse(oldParentId);
        }

        /// <summary>
        /// Сохраняет папку в базу данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно, иначе - FALSE</returns>
        private bool Save()
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
        /// Удаляет папку на сервере и из базы данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно, иначе - FALSE</returns>
        public bool RemoveOnServer()
        {
            bool result = true;
            try
            {
                ExecuteNonQuery("DELETE from dirs where id='" + this.Id + "'");
                Controller.ApiTwoSafe.removeDir(this.Id);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Удаляет папку в локальной папке и из базы данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно, иначе - FALSE</returns>
        public bool RemoveOnClient()
        {
            bool result = true;
            try
            {
                ExecuteNonQuery("DELETE from dirs where id='" + this.Id + "'");
                this.SetPath();
                Directory.Delete(this.Path, true);
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
            if (this._path == null)
            {
                string result = "";
                List<string> path = new List<string>();

                Dir parentDir = this;

                while (parentDir.Id != Properties.Settings.Default.RootId)
                {
                    path.Add(parentDir.Name);
                    parentDir = FindById(parentDir.Parent_id);
                    //if (parentDir == null) break;
                }

                for (int i = path.Count - 1; i >= 0; --i)
                {
                    result += "\\" + path[i];
                }

                this._path = Properties.Settings.Default.UserFolderPath + result;
            }
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
        public static Dir FindById(long id)
        {
            Model.Dir dir = null;
            if (id != Properties.Settings.Default.RootId)
            {
                SQLiteConnection connection = new SQLiteConnection(dbName);
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs WHERE id='" + id.ToString() + "'", connection);
                SQLiteDataReader reader = command.ExecuteReader();
                reader.Read();

                dir = new Model.Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));
                //dir.SetPath();

                reader.Close();
                connection.Close();
            }
            else
            {
                dir = new Dir(Properties.Settings.Default.RootId, 0, "root");
            }

            return dir;
        }

        /// <summary>
        /// Находит папку в базе данных по названию и ID родительской папки
        /// </summary>
        /// <param name="name">Имя папки</param>
        /// <param name="parent_id">ID родительской папки</param>
        /// <returns>Возвращает объект папки</returns>
        public static Dir FindByNameAndParentId(string name, long parent_id)
        {
            Dir result = null;


            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs WHERE name='" + name + "' AND parent_id='" + parent_id.ToString() + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            try
            {
                reader.Read();
                result = new Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));
            }
            catch
            {
            }

            reader.Close();
            connection.Close();

            //result = new Dir(Properties.Settings.Default.RootId, 0, "root");
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

            dir = FindByNameAndParentId(parts[0], Properties.Settings.Default.RootId);
            for (int i = 1; i < parts.Length; ++i)
            {
                dir = FindByNameAndParentId(parts[i], dir.Id);
            }

            return dir;
        }

        /// <summary>
        /// Находит родительскую папку по полному пути
        /// </summary>
        /// <param name="path">Полный путь до папки (включая саму папку)</param>
        /// <returns></returns>
        public static Dir FindParentByPath(String path)
        {
            Dir result;
            string[] parts = path.Substring(Properties.Settings.Default.UserFolderPath.Length + 1).Split('\\');

            if (parts.Length == 1)
            {
                result = new Dir(Properties.Settings.Default.RootId, 0, "root");
            }
            else
            {
                result = FindByNameAndParentId(parts[0], Properties.Settings.Default.RootId);
                for (int i = 1; i < parts.Length - 1; ++i)
                {
                    result = FindByNameAndParentId(parts[i], result.Id);
                }
            }

            return result;
        }

        /// <summary>
        /// Создает папку на сервере и сохраняет ее в БД
        /// </summary>
        /// <param name="parent_id">ID родительской папки</param>
        /// <param name="fullPath">Полный путь данной папки (включая саму папку)</param>
        /// <returns></returns>
        public static Dir Upload(long parent_id, string fullPath)
        {
            fullPath = System.IO.Path.GetFileName(fullPath);
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.makeDir(parent_id, fullPath, null)["response"];
            Dir dir = new Model.Dir(json["dir_id"], parent_id, fullPath);
            dir.Save();
            return dir;
        }

        /// <summary>
        /// Переименовывает папку в локальной папке и в базе данных
        /// </summary>
        /// <param name="newName">Новое имя папки (не включая путь).</param>
        public void RenameOnClient()
        {
            if (_oldName != null)
            {
                SQLiteConnection connection = new SQLiteConnection(dbName);
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("UPDATE dirs SET name='" + this._name + "' WHERE id='" + this._id.ToString() + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();

                string newPath = this.Path;
                this._name = _oldName;
                this._path = null;
                string oldPath = this.Path;
                Directory.Move(oldPath, newPath);
            }
        }

        /// <summary>
        /// Переименовывает папку на сервере и в БД
        /// </summary>
        /// <param name="newName">Новый полный путь до папки (включая саму папку)</param>
        public void RenameOnServer(string newName)
        {
            this._oldName = this._name;
            this._name = System.IO.Path.GetFileName(newName);

            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("UPDATE dirs SET name='" + this._name + "' WHERE id='" + this._id.ToString() + "'", connection);
            command.ExecuteNonQuery();
            connection.Close();

            Controller.ApiTwoSafe.moveDir(this.Parent_id, this.Id, this.Name, null);
        }

        /// <summary>
        /// Перемещает папку в локальной папке и в базе данных
        /// </summary>
        public void MoveOnClient()
        {
            if (_old_parent_id != null)
            {
                SQLiteConnection connection = new SQLiteConnection(dbName);
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("UPDATE dirs SET parent_id='" + this._parent_id + "' WHERE id='" + this._id.ToString() + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();

                string newPath = this.Path;
                this._parent_id = _old_parent_id;
                this._path = null;
                string oldPath = this.Path;
                Directory.Move(oldPath, newPath);
            }
        }

        /// <summary>
        /// Создает папку в локальной папке и сохраняет ее в БД
        /// </summary>
        public void Download()
        {
            Directory.CreateDirectory(this.Path);
            this.Save();
        }

        public static List<Dir> All(long parent_id)
        {
            List<Dir> result = new List<Dir>();
            Dir temp;

            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM dirs WHERE parent_id='" + parent_id.ToString() + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();

            while (reader.Read())
            {
                temp = new Dir(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));
                temp.SetPath();
                result.Add(temp);
            }

            reader.Close();
            connection.Close();

            return result;
        }

        /// <summary>
        /// Возвращает все папки из базы данных
        /// </summary>
        /// <returns>Возвращает список папок: упорядоченных по возрастанию level.</returns>
        /*public static List<Dir> All()
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
        }*/

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
            get
            {
                if (_path == null)
                {
                    this.SetPath();
                }
                return _path;
            }
        }
    }
}
