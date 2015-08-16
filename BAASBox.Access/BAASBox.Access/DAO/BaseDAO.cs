using System;

namespace BAASBox.Access.DAO
{
	public class BaseDAO : IDisposable
	{
		protected BAASBoxConfig config;

		public BaseDAO (BAASBoxConfig config)
		{
			this.config = config;
		}

		public void Dispose ()
		{
		}
	}
}

