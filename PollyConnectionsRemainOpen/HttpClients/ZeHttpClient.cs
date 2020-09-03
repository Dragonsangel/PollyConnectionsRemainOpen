using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyConnectionsRemainOpen.HttpClients
{
	public class ZeHttpClient : INoPollyHttpClient, IPollyHttpClient
	{
		private readonly HttpClient httpClient;

		public ZeHttpClient(HttpClient httpClient)
		{
			this.httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		}

		public Task<HttpResponseMessage> GetDataAsync(String endpoint)
		{
			return this.httpClient.GetAsync(endpoint);
		}
	}
}