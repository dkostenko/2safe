using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Model
{
    class Dir
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
