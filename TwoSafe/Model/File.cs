using System.Collections;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class File : ActiveRecord
    {
        private string _name, _chksum;
        private long _id, _parent_id, _version_id;
        private int _size;

        public File(long id, long parent_id, string name, long version_id, string chksum, int size)
        {
            this._id = id;
            this._parent_id = parent_id;
            this._name = name;
            this._version_id = version_id;
            this._chksum = chksum;
            this._size = size;
        }

        public File(string id, string parent_id, string name, string version_id, string chksum, string size)
        {
            this._id = long.Parse(id);
            this._parent_id = long.Parse(parent_id);
            this._name = name;
            this._version_id = long.Parse(version_id);
            this._chksum = chksum;
            this._size = int.Parse(size);
        }


        /// <summary>
        /// Сохраняет файл в базу данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно</returns>
        public bool Save()
        {
            bool result = true;
            string values = "'" + this.Id + "', '" + 
                                  this.Parent_id + "', '" +
                                  this.Name + "', '" +
                                  this.Version_id + "', '" +
                                  this.Chksum + "', '" +
                                  this.Size + "'"; ;

            try
            {
                ExecuteNonQuery("insert into files(id, parent_id, name, version_id, chksum, size) values(" + values + ");");
            }
            catch
            {
                result = false;
            }
            return result;
        }

        /// <summary>
        /// Удаляет файл из базы данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно, иначе - FALSE</returns>
        public bool Remove()
        {
            bool result = true;
            try
            {
                ExecuteNonQuery("DELETE from files where id='" + this.Id + "'");
            }
            catch
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Находит полный путь до файла в синхронизируемой папке
        /// </summary>
        public string GetPath()
        {
            string result = "";
            ArrayList path = new ArrayList();

            Dir parentDir = Model.Dir.FindById(this._parent_id.ToString());

            while (parentDir != null)
            {
                path.Add(parentDir.Name);
                parentDir = Model.Dir.FindById(parentDir.Parent_id.ToString());
                if (parentDir == null) break;
            }

            for (int i = path.Count - 1; i >= 0; --i)
            {
                result += "\\" + path[i].ToString();
            }

            return Properties.Settings.Default.UserFolderPath + result + "\\" + this.Name;
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

            Model.File file = new Model.File(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2), reader.GetInt64(3), reader.GetString(4), reader.GetInt32(5));

            reader.Close();
            connection.Close();

            return file;
        }

        /// <summary>
        /// Находит файл в базе данных по названию и ID родительской папки
        /// </summary>
        /// <param name="name">Имя файла</param>
        /// <param name="dir_id">ID родительской папки</param>
        /// <returns>Возвращает объект файла</returns>
        public static File FindByNameAndParentId(string name, string dir_id)
        {
            SQLiteConnection connection = new SQLiteConnection(dbName);
            connection.Open();
            SQLiteCommand command = new SQLiteCommand("SELECT * FROM files WHERE name='" + name + "' AND dir_id='" + dir_id + "'", connection);
            SQLiteDataReader reader = command.ExecuteReader();
            reader.Read();
            File file = new Model.File(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2), reader.GetInt64(3), reader.GetString(4), reader.GetInt32(5));

            reader.Close();
            connection.Close();
            return file;
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

        public int Size
        {
            get { return _size; }
        }
    }
}
