using System;

namespace tosafe
{
	public class Response
	{
		public string success { get; set; }
		public string available { get; set; }
		public string token { get; set; }
		public string id { get; set; }
	}

	public class Json
	{
		public Response response { get; set; }
		public string error_msg { get; set; }
		public string error_code { get; set; }
	}
}

