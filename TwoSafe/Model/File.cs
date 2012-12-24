using System.Collections;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class File : ActiveRecord
    {
        private string _name;
        private long _id, _dir_id;

        public File(long id, long dir_id, string name)
        {
            this._id = id;
            this._dir_id = dir_id;
            this._name = name;
        }

        public File(string id, string dir_id, string name)
        {
            this._id = long.Parse(id);
            this._dir_id = long.Parse(dir_id);
            this._name = name;
        }


        /// <summary>
        /// Сохраняет файл в базу данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно</returns>
        public bool Save()
        {
            bool result = true;
            string values = "'" + this.Id + "', '" + this.Dir_id + "', '" + this.Name + "'"; ;

            try
            {
                ExecuteNonQuery("insert into files(id, dir_id, name) values(" + values + ");");
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

            Dir parentDir = Model.Dir.FindById(this._dir_id.ToString());

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

            Model.File file = new Model.File(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));

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
            File file = new File(reader.GetInt64(0), reader.GetInt64(1), reader.GetString(2));

            reader.Close();
            connection.Close();
            return file;
        }

        public long Id
        {
            get { return _id; }
        }

        public long Dir_id
        {
            get { return _dir_id; }
        }

        public string Name
        {
            get { return _name; }
        }
    }
}
