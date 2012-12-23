using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Controller
{
    class Files : Db
    {
        /// <summary>
        /// Добавляет файл в пазу данных
        /// </summary>
        /// <param name="id">ID файла на сайте 2safe</param>
        /// <param name="parent_id">parent_id папки на сайте 2safe</param>
        /// <param name="name">Название папки</param>
        /// <returns>Возвращает TRUE, если операция прошла успешно</returns>
        public static bool add(string id, string dir_id, string name)
        {
            bool returnCode = true;
            string values = "'" + id + "', '" + dir_id + "', '" + name + "'"; ;

            try
            {
                executeNonQuery("insert into files(id, dir_id, name) values(" + values + ");");
            }
            catch (Exception fail)
            {
                returnCode = false;
            }
            return returnCode;
        }
    }
}
