using System;
using System.Threading.Tasks;
using BAASBox.Access.DAO;
using BAASBox.Access.DTO;
using BAASBox.Access.AppState;

namespace BAASBox.Access.Business
{
	public class AuthLogic : BaseLogic
	{
		public AuthLogic(AuthState auth, BAASBoxConfig config) : base(auth, config)
		{
		}

		public void SignOut()
		{
			auth.Clear();
		}

		public async Task<SignInData> SignInAsync(string username, string password)
		{
			BBResponse<SignInData> response = null;
			try {
				using (var authDAO = new AuthorisationDAO(config)) {
					response = await authDAO.SignInGetTokenAsync (username, password);
				}
			} catch {
				// TODO: error... so... throw it?
			}

			if (response != null && response.data != null && response.data.X_BB_SESSION != null) {
				auth.SessionId = response.data.X_BB_SESSION;
				auth.UserName = response.data.user.name;
				auth.UserStatus = response.data.user.status;
				return response.data;

			} else {
				auth.Clear ();
				return null;
			}
		}
	}
}

