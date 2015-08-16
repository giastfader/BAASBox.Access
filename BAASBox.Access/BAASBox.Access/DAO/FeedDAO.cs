using System;
using System.Threading.Tasks;
using Flurl.Http;
using BAASBox.Access.DTO;

namespace BAASBox.Access.DAO
{
	public class FeedDAO : BaseDAO
	{
		public FeedDAO(BAASBoxConfig config) : base(config)
		{
		}

		public async Task<BBResponse<FollowResponseData>> Follow(string username, string sessionId)
		{
			
			var result = await config.EndpointFollow(username)
				.WithHeader("X-BB-SESSION", sessionId)
				.PostAsync()
				.ReceiveJson<BBResponse<FollowResponseData>>();

			return result;
		}

		public async Task<BBResponse<string>> Unfollow(string username, string sessionId)
		{
			var endpoint = config.EndpointFollow (username);
			var result = await endpoint
				.WithHeader("X-BB-SESSION", sessionId)
				.DeleteAsync()
				.ReceiveJson<BBResponse<string>>();

			return result;
		}

		public async Task<BBResponse<FollowerData[]>> GetFollowers(string username, string sessionId)
		{
			var result = await config.EndpointFollowers(username)
				.WithHeader("X-BB-SESSION", sessionId)
				.GetAsync()
				.ReceiveJson<BBResponse<FollowerData[]>>();

			return result;
		}

		public async Task<BBResponse<FollowerData[]>> GetFollowing(string username, string sessionId)
		{
			var result = await config.EndpointFollowing(username)
				.WithHeader("X-BB-SESSION", sessionId)
				.GetAsync()
				.ReceiveJson<BBResponse<FollowerData[]>>();

			return result;
		}
	}
}

