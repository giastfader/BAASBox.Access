using System;
using System.Collections.Generic;

namespace BAASBox.Access.AppState
{
	public abstract class BaseAppState
	{
		protected IDictionary<string, object> properties = new Dictionary<string, object> ();

		protected T Get<T>(string key) {
			if (properties.ContainsKey (key)) {
				return (T)properties [key];
			} else {
				return default (T);
			}
		}

		protected void Set<T>(string key, T value) {
			properties [key] = value;
		}

		public void StoreTo(IDictionary<string,object> props)
		{
			foreach (var key in properties.Keys) {
				props[key] = properties[key];
			}
		}

		public void RestoreFrom(IDictionary<string,object> props)
		{
			properties.Clear ();
			foreach (var key in props.Keys) {
				properties[key] = props[key];
			}
		}
	}
}

