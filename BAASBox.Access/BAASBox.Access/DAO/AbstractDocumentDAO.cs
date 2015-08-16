using System;
using System.Threading.Tasks;
using BAASBox.Access.DTO;
using Flurl.Http;
using System.Collections.Generic;

namespace BAASBox.Access.DAO
{
	public abstract class AbstractDocumentDAO<T> : BaseDAO where T : AbstractCrudObject
	{
		protected string typename;

		public AbstractDocumentDAO (BAASBoxConfig config, string typename) : base(config)
		{
			this.typename = typename;
		}

		public async Task<BBResponse<T>> CreateAsync(T document, string sessionId)
		{
			var result = await config.EndpointDocument(typename)
				.WithHeader("X-BB-SESSION", sessionId)
				.PostJsonAsync(document)
				.ReceiveJson<BBResponse<T>>();

			return result;
		}

		public async Task<BBResponse<T>> UpdateAsync(T document, string sessionId)
		{
			var result = await config.EndpointDocument(typename, document.id)
				.WithHeader("X-BB-SESSION", sessionId)
				.PutJsonAsync(document)
				.ReceiveJson<BBResponse<T>>();

			return result;
		}

		public async Task<BBResponse<T>> CreateOrUpdateAsync(T document, string sessionId)
		{
			if (document.id != null) {
				return await UpdateAsync (document, sessionId);
			} else {
				return await CreateAsync (document, sessionId);
			}
		}

		public async Task<BBResponse<string>> DeleteAsync(T entry, string sessionId)
		{
			var result = await config.EndpointDocument(typename, entry.id)
				.WithHeader("X-BB-SESSION", sessionId)
				.DeleteAsync()
				.ReceiveJson<BBResponse<string>>();

			return result;
		}

		public async Task<BBResponse<Object>> ShareAsync(string postId, string username, string sessionId, string role = null)
		{
			role = role ?? ("friends_of_" + username);
			var query = config.EndpointShare (typename, postId, role);
			var result = await query
				.WithHeader("X-BB-SESSION", sessionId)
				.PutAsync()
				.ReceiveJson<BBResponse<Object>>();
			return result;
		}

		public async Task<BBResponse<Object>> UnshareAsync(string postId, string username, string sessionId, string role = null)
		{
			role = role ?? ("friends_of_" + username);
			var result = await config.EndpointShare(typename, postId, role)
				.WithHeader("X-BB-SESSION", sessionId)
				.DeleteAsync()
				.ReceiveJson<BBResponse<Object>>();
			return result;
		}

		public async Task<BBResponse<T>> GetAsync(string id, string sessionId) {
			string endpoint = config.EndpointDocument (typename, id);

			var result = await endpoint
				.WithHeader ("X-BB-SESSION", sessionId)
				.GetJsonAsync<BBResponse<T>> ();

			return result;
		}

		public async Task<BBResponse<IList<T>>> ListAsync(string sessionId, string where = null)
		{
			string endpoint = config.EndpointDocument (typename);

			if (where != null) {
				var whereEncoded = Uri.EscapeUriString (where);
				endpoint += "?" + whereEncoded;
			}

			var result = await endpoint
				.WithHeader ("X-BB-SESSION", sessionId)
				.GetJsonAsync<BBResponse<IList<T>>> ();

			return result;
		}

		public async Task<BBResponse<IList<T>>> ListRecentAsync(int count, string sessionId, int page = 0)
		{
			var recordsPerPageClause = "recordsPerPage=" + count;
			var pageClause = "page=" + page;
			var orderByClause = "orderBy=_creation_date desc";

			var query = string.Format("{0}?{1}&{2}&{3}",
				config.EndpointDocument(typename),
				recordsPerPageClause,
				pageClause,
				orderByClause);

			var result = await query
				.WithHeader ("X-BB-SESSION", sessionId)
				.GetJsonAsync<BBResponse<IList<T>>> ();

			return result;
		}


	}
}

