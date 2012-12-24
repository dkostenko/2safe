using System;
using System.Data.SQLite;

namespace TwoSafe.Model
{
    class Dir : Db
    {
        private string name;
        private long id, parent_id;

        public Dir(long id, long parent_id, string name)
        {
            this.id = id;
            this.parent_id = parent_id;
            this.name = name;
        }

        public Dir(string id, string parent_id, string name)
        {
            this.id = long.Parse(id);
            this.parent_id = long.Parse(parent_id);
            this.name = name;
        }

        /// <summary>
        /// Сохраняет папку в базу данных
        /// </summary>
        /// <returns>Возвращает TRUE, если операция прошла успешно</returns>
        public bool Save()
        {
            bool result = true;
            string values = "'" + this.Id + "', '" + this.Parent_id + "', '" + this.Name + "'";

            try
            {
                executeNonQuery("insert into dirs(id, parent_id, name) values(" + values + ");");
            }
            catch
            {
                result = false;
            }
            return result;
        }


        /// <summary>
        /// Находит папку в базе данных по ID
        /// </summary>
        /// <param name="id">ID папки на сайте 2safe</param>
        /// <returns>Возвращает объект папки</returns>
        public static Dir FindById(string id)
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

        public long Id
        {
            get { return id; }
        }

        public long Parent_id
        {
            get { return parent_id; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}
