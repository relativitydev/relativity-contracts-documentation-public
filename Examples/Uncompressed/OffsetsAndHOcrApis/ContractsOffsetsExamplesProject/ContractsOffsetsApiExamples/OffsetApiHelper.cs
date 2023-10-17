using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ContractsOffsetsApiExamples
{
	/// <summary>
	/// Helper class for making Contracts Offsets REST requests.
	/// </summary>
	public class OffsetApiHelper : IDisposable
	{
		private HttpClient _httpClient;

		/// <summary>
		/// Initializes a new instance of <see cref="OffsetApiHelper"/>.
		/// </summary>
		/// <param name="hostname">The hostname of the environment to send requests to.</param>
		/// <param name="username">The username to use for authenticating the request (should be the email address of a Relativity user).</param>
		/// <param name="password">The password to use for authenticating the request.</param>
		/// <param name="useHttps">(Optional) Whether to use HTTPS to make the request. Defaults to <see langword="true"/>.</param>
		public OffsetApiHelper(string hostname, string username, string password, bool useHttps = true)
		{
			_httpClient = new HttpClient();
			var base64EncodedAuthenticationString = Convert.ToBase64String(ASCIIEncoding.UTF8.GetBytes($"{username}:{password}"));

			string scheme = useHttps ? "https" : "http";
			_httpClient.BaseAddress = new Uri($"{scheme}://{hostname}/Relativity.Rest/API/");
			_httpClient.DefaultRequestHeaders.Add("X-CSRF-Header", "-");
			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", base64EncodedAuthenticationString);
		}

		public void Dispose()
		{
			_httpClient.Dispose();
		}

		/// <summary>
		/// Gets the offsets associated with the specified Document.
		/// </summary>
		/// <param name="workspaceId">The ID of the Workspace.</param>
		/// <param name="documentId">The Artifact ID of the Document.</param>
		/// <returns>An awaitable <see cref="Task"/> that resolves to the offsets associated with the Document.</returns>
		public async Task<string> GetOffsets(int workspaceId, int documentId)
		{
			var url = new Uri($"contracts/v1/offsets/{workspaceId}/document/{documentId}", UriKind.Relative);
			var response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

		/// <summary>
		/// Updates the offsets specified in <paramref name="requestBody"/> if they exist
		/// and creates them if they don't.
		/// </summary>
		/// <param name="workspaceId">The ID of the Workspace.</param>
		/// <param name="documentId">The Artifact ID of the Document the offsets should be associated with.</param>
		/// <param name="requestBody">The body of the request, which should contain the offsets to create/update.</param>
		/// <returns>An awaitable <see cref="Task"/> that resolves to the offsets created/updated.</returns>
		/// <remarks>
		/// The <paramref name="requestBody"/> can contain a mix of existing and nonexisting
		/// offsets.
		/// </remarks>
		public async Task<string> UpsertOffsets(int workspaceId, int documentId, string requestBody)
		{
			var url = new Uri($"contracts/v1/offsets/{workspaceId}/document/{documentId}", UriKind.Relative);

			// NOTE: "application/json" would normally be passed as the value of the "Content-Type" header key-value pair.
			var response = await _httpClient.PostAsync(url, new StringContent(requestBody, Encoding.UTF8, "application/json"));
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}

		/// <summary>
		/// Deletes the offset with the specified ID associated with the specified Document.
		/// </summary>
		/// <param name="workspaceId">The ID of the Workspace.</param>
		/// <param name="documentId">The Artifact ID of the Document that the offset is associated with.</param>
		/// <param name="offsetId">
		/// The ID of the offset. This corresponds to the "Id" property of the objects returned by <see cref="GetOffsets(int, int)"/>
		/// and the OffsetId column of the [heretik].[FieldOffset] table in the Workspace's database.
		/// </param>
		/// <returns>An awaitable <see cref="Task"/> that does not resolve to a value.</returns>
		public async Task DeleteOffset(int workspaceId, int documentId, int offsetId)
		{
			var url = new Uri($"contracts/v1/offsets/{workspaceId}/document/{documentId}/{offsetId}", UriKind.Relative);
			var response = await _httpClient.DeleteAsync(url);
			response.EnsureSuccessStatusCode();
		}
	}
}
