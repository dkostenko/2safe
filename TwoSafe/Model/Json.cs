using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TwoSafe.Model
{
    public class Response
    {
        public bool success { get; set; }
        public string available { get; set; }
        public string token { get; set; }
        public string id { get; set; }
        public Personal personal { get; set; }
    }

    public class Personal
    {
        public string email { get; set; }
        public string lang { get; set; }
        public string last_name { get; set; }
        public string first_name { get; set; }
        public string avatar { get; set; }

        public override string ToString()
        {
            return string.Format("[Personal: email={0}, lang={1}, last_name={2}, first_name={3}, avatar={4}]", email, lang, last_name, first_name, avatar);
        }
    }

    public class Json
    {
        public Response response { get; set; }
        public string error_code { get; set; }
    }
}
