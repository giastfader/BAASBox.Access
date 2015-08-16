using System;
using System.Runtime.Serialization;

namespace BAASBox.Access.DTO
{
	[DataContract]
	public class FollowerData
	{
		[DataMember]
		public SignInUser user { get; set; }

		[DataMember]
		public string signUpDate { get; set; }

	}
}

