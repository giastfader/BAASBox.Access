using System;

namespace BAASBox.Access.DAO
{
	public class BAASBoxConfig
	{
		public BAASBoxConfig (string server, int port = 443, string appcode = "1234567890")
		{
			Server = server;
			Port = port;
			AppCode = appcode;

			Endpoint = Server + ":" + Port + "/";
			EndpointLogin = Endpoint + "login";
		}

		public string Server;
		public int Port = 443;
		public string AppCode;

		public string EndpointLogin;
		public string Endpoint;

		public string EndpointDocument(string typename, string id = null) {
			if (id == null) {
				return Endpoint + "document/" + typename;
			} else {
				return EndpointDocument (typename) + "/" + id;
			}
		}

		public string EndpointFollow(string username)
		{
			return Endpoint + "follow/" + username;
		}

		public string EndpointFollowers(string username)
		{
			return Endpoint + "followers/" + username;
		}

		public string EndpointFollowing(string username)
		{
			return Endpoint + "following/" + username;
		}

		public string EndpointShare(string typename, string postId, string role)
		{
			return EndpointDocument (typename, postId) + "/read/role/" + role;
		}
	}
}

