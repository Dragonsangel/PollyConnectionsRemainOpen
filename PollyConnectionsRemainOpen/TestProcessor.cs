using Microsoft.Extensions.Logging;
using PollyConnectionsRemainOpen.HttpClients;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace PollyConnectionsRemainOpen
{
	public class TestProcessor : ITestProcessor
	{
		private readonly ILogger<TestProcessor> logger;
		private readonly IZeHttpClient pollyHttpClient;
		private readonly INoPollyHttpClient noPollyHttpClient;

		public TestProcessor(ILogger<TestProcessor> logger,
							 IPollyHttpClient pollyHttpClient,
							 INoPollyHttpClient noPollyHttpClient)
		{
			this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
			this.pollyHttpClient = pollyHttpClient ?? throw new ArgumentNullException(nameof(pollyHttpClient));
			this.noPollyHttpClient = noPollyHttpClient ?? throw new ArgumentNullException(nameof(noPollyHttpClient));
		}

		public async Task<Boolean> PerformCall(Boolean withPolly)
		{
			try
			{
				using (HttpResponseMessage response = withPolly ?
													  await this.pollyHttpClient.GetDataAsync("failingRoute/fail") :
													  await this.noPollyHttpClient.GetDataAsync("failingRoute/fail"))
				{
					if (response.IsSuccessStatusCode)
					{
						return true;
					}
				}
			}
			catch (Exception exc)
			{
				this.logger.LogError(exc, "Exception happened");
			}

			return false;
		}
	}
}