using System;
using System.Runtime.Serialization.Json;

namespace tosafe
{
	[DataContract]
	public class Auth
	{
		[DataMember]
		internal string name;
		
		[DataMember]
		internal int age;
	}
}

