using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using BAASBox.Access.DAO;
using BAASBox.Access.AppState;

namespace BAASBox.Access.Business
{
	public class FeedLogic : BaseLogic
	{
		public FeedLogic(AuthState auth, BAASBoxConfig config) : base(auth, config)
		{
		}

		public async Task<bool> FollowAsync(string username)
		{
			var session = auth.SessionId;

			using (var dao = new FeedDAO (config)) {
				var result = await dao.FollowAsync (username, session);
				var response = result.data;

				if (result.result.ToLower () == "ok" &&
					response.user != null &&
					response.user.name == username) {

					return true;

				} else {
					return false;
				}
			}
		}

		public async Task<bool> UnfollowAsync(string username)
		{
			var session = auth.SessionId;
			using (var dao = new FeedDAO (config)) {
				var result = await dao.UnfollowAsync (username, session);
				var response = result.data;

				if (result.result.ToLower () == "ok") {
					return true;

				} else {
					return false;
				}
			}
		}

		public async Task<IEnumerable<string>> GetFollowingAsync()
		{
			var session = auth.SessionId;
			var user = auth.UserName;
			using (var dao = new FeedDAO (config)) {
				var result = await dao.GetFollowingAsync (user, session);
				var following = result.data;
				return following.Select (f => f.user.name);
			}
		}

		public async Task<IEnumerable<string>> GetFollowersAsync()
		{
			var session = auth.SessionId;
			var user = auth.UserName;
			using (var dao = new FeedDAO (config)) {
				var result = await dao.GetFollowersAsync (user, session);
				var followers = result.data;
				return followers.Select (f => f.user.name);
			}
		}
	}
}

