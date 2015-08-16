using System;
using System.Runtime.Serialization;

namespace BAASBox.Access.DTO
{
	[DataContract]
	public class SignInData : BBData
	{
		[DataMember]
		public SignInUser user { get; set; }

		[DataMember]
		public string signUpDate { get; set; }

		[DataMember (Name = "X-BB-SESSION")]
		public string X_BB_SESSION { get; set; }
		//  should have hyphens

		public override bool MayDelete {
			get {
				return false;
			}
		}
	}

	[DataContract]
	public class SignInUser
	{
		[DataMember]
		public string name { get; set; }

		[DataMember]
		public string status { get; set; }

		[DataMember]
		SignInRole[] roles { get; set; }

	}

	[DataContract]
	public class SignInRole
	{
		[DataMember]
		string name { get; set; }
	}
}
