using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Model
{
    class File : Db
    {
        private string name;
        private long id, dir_id;

        public File(long id, long dir_id, string name)
        {
            this.id = id;
            this.dir_id = dir_id;
            this.name = name;
        }

        public File(string id, string dir_id, string name)
        {
            this.id = long.Parse(id);
            this.dir_id = long.Parse(dir_id);
            this.name = name;
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
                executeNonQuery("insert into files(id, dir_id, name) values(" + values + ");");
            }
            catch
            {
                result = false;
            }
            return result;
        }

        public long Id
        {
            get { return id; }
        }

        public long Dir_id
        {
            get { return dir_id; }
        }

        public string Name
        {
            get { return name; }
        }
    }
}
