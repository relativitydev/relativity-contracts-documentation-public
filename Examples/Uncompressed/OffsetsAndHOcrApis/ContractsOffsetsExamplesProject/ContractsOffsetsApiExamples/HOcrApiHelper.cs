using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ContractsOffsetsApiExamples
{
	/// <summary>
	/// Helper class for making Contracts hOCR data REST requests.
	/// </summary>
	public class HOcrApiHelper : IDisposable
	{
		private HttpClient _httpClient;

		/// <summary>
		/// Initializes a new instance of <see cref="HOcrApiHelper"/>.
		/// </summary>
		/// <param name="hostname">The hostname of the environment to send requests to.</param>
		/// <param name="username">The username to use for authenticating the request (should be the email address of a Relativity user).</param>
		/// <param name="password">The password to use for authenticating the request.</param>
		/// <param name="useHttps">(Optional) Whether to use HTTPS to make the request. Defaults to <see langword="true"/>.</param>
		public HOcrApiHelper(string hostname, string username, string password, bool useHttps = true)
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
		/// Gets the hOCR data associated with the specified page of a Document.
		/// </summary>
		/// <param name="workspaceId">The ID of the Workspace.</param>
		/// <param name="documentId">The Artifact ID of the Document.</param>
		/// <param name="pageNumber">The page number to get hOCR data for.</param>
		/// <returns>
		/// An awaitable <see cref="Task"/> that resolves to the hOCR data for the specified Document page.
		/// </returns>
		public async Task<string> GetHOcrData(int workspaceId, int documentId, int pageNumber)
		{
			var url = new Uri($"contracts/v1/ocr/{workspaceId}/document/{documentId}/page/{pageNumber}", UriKind.Relative);
			var response = await _httpClient.GetAsync(url);
			response.EnsureSuccessStatusCode();

			return await response.Content.ReadAsStringAsync();
		}
	}
}
