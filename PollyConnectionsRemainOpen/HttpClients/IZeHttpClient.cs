using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyConnectionsRemainOpen.HttpClients
{
	public interface IZeHttpClient
	{
		Task<HttpResponseMessage> GetDataAsync(String endpoint);
	}
}