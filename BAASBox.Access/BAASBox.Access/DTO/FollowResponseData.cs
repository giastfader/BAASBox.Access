using System;
using System.Collections.Generic;

namespace BAASBox.Access.DTO
{
	public class FollowResponseData
	{
		public UserData user { get; set; }
		public DateTime signUpDate { get; set; }
		public Object visibleByFriends { get; set; }
	}

	public class UserData
	{
		public string name { get; set; }
		public List<RoleData> roles { get; set; }
		public string status { get; set; }
	}

	public class RoleData
	{
		public string name { get; set; }
	}
}

