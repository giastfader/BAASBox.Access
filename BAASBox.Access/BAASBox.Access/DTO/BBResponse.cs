using System;
using System.Runtime.Serialization;

namespace BAASBox.Access.DTO
{
	[DataContract]
	public class BBResponse<T>
	{
		[DataMember]
		public string result {get;set;}

		[DataMember]
		public T data {get;set;}

		[DataMember]
		public int http_code {get;set;}
	}
}

