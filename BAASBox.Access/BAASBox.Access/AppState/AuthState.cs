using System;

namespace BAASBox.Access.AppState
{
	public class AuthState : BaseAppState
	{
		public string SessionId
		{
			get {
				return Get<string> ("sessionId");
			}
			set {
				var was = Get<string>("sessionId");
				Set ("sessionId", value);
				if (OnAuthChange != null && (was != value)) {
					OnAuthChange (IsAuthorised);
				}
			}
		}

		public string UserName {
			get {
				return Get<string> ("userName");
			}
			set {
				Set ("userName", value);
			}
		}

		public string UserStatus {
			get {
				return Get<string> ("userStatus");
			}
			set {
				Set ("userStatus", value);
			}
		}

		public void Clear()
		{
			UserName = null;
			UserStatus = null;
			SessionId = null;
		}

		public event Action<bool> OnAuthChange;

		public bool IsAuthorised { get { return SessionId != null; } }
	}
}

