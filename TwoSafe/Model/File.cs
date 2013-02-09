using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;

namespace TwoSafe.Model
{
    class File : ActiveRecord
    {
        private string _name, _chksum, _old_name, _path;
        private long _id, _parent_id, _version_id, _mtime, _old_id, _old_parent_id;
        private int _size;

        public File(long id, long parent_id, string name)
        {
            this._id = id;
            this._parent_id = parent_id;
            this._name = name;
        }

        public File(string id, long parent_id, string name)
        {
            this._id = long.Parse(id);
            this._parent_id = parent_id;
            this._name = name;
        }

        public File(string id, string parent_id, string name)
        {
            this._id = long.Parse(id);
            this._parent_id = long.Parse(parent_id);
            this._name = name;
        }

        public File(string newId, string newParentId, string newName, string oldId, string oldParentId, string oldName, string versionId)
        {
            this._id = long.Parse(newId);
            this._parent_id = long.Parse(newParentId);
            this._name = newName;
            this._old_parent_id = long.Parse(oldParentId);
            this._old_id = long.Parse(oldId);
            this._old_name = oldName;
            this._version_id = long.Parse(versionId);
        }


        public File(long id, long parent_id, string name, long version_id, string chksum, int size, long mtime)
        {
            this._id = id;
            this._parent_id = parent_id;
            this._name = name;
            this._version_id = version_id;
            this._chksum = chksum;
            this._size = size;
            this._mtime = mtime;
        }

        /// <summary>
        /// Сохраняет файл в базу данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно</returns>
        private bool Save()
        {
            bool result = true;
            string values = "'" + this.Id + "', '" + 
                                  this.Parent_id + "', '" +
                                  this.Name + "', '" +
                                  this.Version_id + "', '" +
                                  this.Chksum + "', '" +
                                  this.Size + "', '" +
                                  this.Mtime + "'"; ;

            try
            {
                ExecuteNonQuery("insert into files(id, parent_id, name, version_id, chksum, size, mtime) values(" + values + ");");
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Удаляет файл на сервере и из базы данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно, иначе - FALSE</returns>
        public bool RemoveOnServer()
        {
            bool result = true;
            try
            {
                ExecuteNonQuery("DELETE FROM files where id='" + this.Id + "'");
                Controller.ApiTwoSafe.removeFile(this.Id);
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Удаляет файл в локальной папке и из базы данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно, иначе - FALSE</returns>
        public bool RemoveOnClient()
        {
            bool result = true;
            try
            {
                System.IO.File.Delete(this.Path);
                ExecuteNonQuery("DELETE FROM files WHERE id='" + this.Id + "'");
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Скачивает файл с сервера в локальную папку и сохраняет его в БД
        /// </summary>
        public void Download()
        {
            Controller.ApiTwoSafe.getFile(this.Id.ToString(), null, this.Path);
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.getProps(this.Id)["response"]["object"];
            this._chksum = json["chksum"];
            this._version_id = long.Parse(json["current_version"]);
            this._mtime = json["mtime"];
            this._size = json["size"];
            this.Save();
        }

        /// <summary>
        /// Загружает файл на сервер и сохраняет его в БД
        /// </summary>
        /// <param name="fullPath">Полный путь файла</param>
        /// <param name="parent_id">ID папки, в которой лежит файл</param>
        public static void Upload(long parent_id, string fullPath)
        {
            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.putFile(parent_id, fullPath, null)["response"]["file"];
            File file = new Model.File(long.Parse(json["id"]), parent_id, json["name"], long.Parse(json["version_id"]), json["chksum"], json["size"], json["mtime"]);
            file.Save();
        }

        /// <summary>
        /// Переименовывает файл на сервере
        /// </summary>
        /// <param name="newName">Полный путь до файла в локальной папке (включая имя файла и его расширение)</param>
        public void RenameOnServer(string newName)
        {
            this._old_name = this._name;
            this._name = System.IO.Path.GetFileName(newName);

            Dictionary<string, dynamic> json = Controller.ApiTwoSafe.moveFile(this.Id, this.Parent_id, this.Name, null)["response"];

            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("UPDATE files SET id='" + json["id"] + "', name='" + this._name + "' WHERE id='" + this._id.ToString() + "'", connection);
            command.ExecuteNonQuery();
            connection.Close();
        }

        /// <summary>
        /// Переименовывает файл в локальной папке и в базе данных
        /// </summary>
        /// <param name="newName">Новое имя файла (не включая путь).</param>
        public void RenameOnClient()
        {
            if (_old_name != null)
            {
                SQLiteConnection connection = new SQLiteConnection(dbName);
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("UPDATE files SET name='" + this._name + "', id='" + this._id + "' WHERE id='" + this._old_id + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();

                string newPath = this.Path;
                this._name = _old_name;
                this._path = null;
                string oldPath = this.Path;
                System.IO.File.Move(oldPath, newPath);
            }
        }

        /// <summary>
        /// Перемещает файл в локальной папке и в базе данных
        /// </summary>
        public void MoveOnClient()
        {
            if (_old_parent_id != null)
            {
                SQLiteConnection connection = new SQLiteConnection(dbName);
                connection.Open();
                SQLiteCommand command = new SQLiteCommand("UPDATE files SET parent_id='" + this.Parent_id + "', id='" + this._id + "' WHERE id='" + this._old_id.ToString() + "'", connection);
                command.ExecuteNonQuery();
                connection.Close();

                string newPath = this.Path;
                this._parent_id = _old_parent_id;
                this._path = null;
                string oldPath = this.Path;
                System.IO.File.Move(oldPath, newPath);
            }
        }

        /// <summary>
        /// Находит полный путь до файла (включая имя файла) в синхронизируемой папке
        /// </summary>
        private void SetPath()
        {
            string result = "";
            List<string> path = new List<string>();

            Dir parentDir = Model.Dir.FindById(this._parent_id);

            while (parentDir.Id != Properties.Settings.Default.RootId)
            {
                path.Add(parentDir.Name);
                parentDir = Model.Dir.FindById(parentDir.Parent_id);
                if (parentDir == null) break;
            }

            for (int i = path.Count - 1; i >= 0; --i)
            {
                result += "\\" + path[i];
            }

            _path = Properties.Settings.Default.UserFolderPath + result + "\\" + this.Name;
        }

        /// <summary>
        /// Находит файл в базе данных по полному пути к файлу
        /// </summary>
        /// <param name="path">Путь к файлу</param>
        /// <returns>Возвращает объект файла или NULL, если файл не найден.</returns>
        public static File FindByPath(String path)
        {
            File file = null;
            Dir dir = null;
            string[] parts = path.Substring(Properties.Settings.Default.UserFolderPath.Length + 1).Split('\\');
            if (parts.Length != 1)
            {
                dir = Dir.FindByNameAndParentId(parts[0], Properties.Settings.Default.RootId);
                if (dir != null)
                {
                    for (int i = 1; i < parts.Length - 1; ++i)
                    {
                        dir = Dir.FindByNameAndParentId(parts[i], dir.Id);
                    }
                    file = FindByNameAndParentId(parts[parts.Length - 1], dir.Id);
                }
            }
            else
            {
                file = FindByNameAndParentId(parts[0], Properties.Settings.Default.RootId);
            }

            return file;
        }

        /// <summary>
        /// Находит файл в базе данных по ID
        /// </summary>
        /// <param name="id">ID файла на сайте 2safe</param>
        /// <returns>Возвращает объект файла</returns>
        public static File FindById(string id)
        {
            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM files WHERE id='" + id + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();

            Model.File file = new Model.File(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2), reader.GetInt64(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt64(6));

            reader.Close();
            connection.Close();

            return file;
        }

        /// <summary>
        /// Находит файл в базе данных по названию и ID родительской папки
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <param name="parent_id">ID родительской папки</param>
        /// <returns>Возвращает объект файла</returns>
        public static File FindByNameAndParentId(string name, long parent_id)
        {
            File result = null;
            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM files WHERE name='" + name + "' AND parent_id='" + parent_id.ToString() + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            try
            {
                reader.Read();
                result = new Model.File(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2), reader.GetInt64(3), reader.GetString(4), reader.GetInt32(5), reader.GetInt64(6));
            }
            catch { }
            reader.Close();
            connection.Close();
            return result;
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

        public string Chksum
        {
            get { return _chksum; }
        }

        public long Version_id
        {
            get { return _version_id; }
        }

        public long Mtime
        {
            get { return _mtime; }
        }

        public int Size
        {
            get { return _size; }
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
