using System;
using BAASBox.Access.DAO;
using BAASBox.Access.AppState;

namespace BAASBox.Access.Business
{
	public abstract class BaseLogic : IDisposable
	{
		protected AuthState auth;
		protected BAASBoxConfig config;

		public BaseLogic (AuthState auth, BAASBoxConfig config)
		{
			this.auth = auth;
			this.config = config;
		}

		public void Dispose()
		{
		}
	}
}

