using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using System.Net;
using Flurl;
using Flurl.Http;
using BAASBox.Access.DTO;

namespace BAASBox.Access.DAO
{
	public class AuthorisationDAO : BaseDAO
	{
		public AuthorisationDAO(BAASBoxConfig config) : base(config)
		{
		}

		public async Task<BBResponse<SignInData>> SignInGetTokenAsync(string username, string password)
		{
			var data = new { username = username, password = password, appcode = config.AppCode };
			var json = await config.EndpointLogin
				.PostUrlEncodedAsync (data)
				.ReceiveJson<BBResponse<SignInData>>();

			return json;
		}
	}
}

