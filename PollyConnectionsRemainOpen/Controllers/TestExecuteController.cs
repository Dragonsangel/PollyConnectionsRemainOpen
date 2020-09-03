using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace PollyConnectionsRemainOpen.Controllers
{
	[ApiController]
	[Route("/test")]
	public class TestExecuteController : ControllerBase
	{
		private readonly ITestProcessor testProcessor;

		public TestExecuteController(ITestProcessor testProcessor)
		{
			this.testProcessor = testProcessor ?? throw new ArgumentNullException(nameof(testProcessor));
		}

		[HttpGet("polly")]
		public async Task<Boolean> PollyCaller()
		{
			return await this.testProcessor.PerformCall(true);
		}

		[HttpGet("nopolly")]
		public async Task<Boolean> NoPollyCaller()
		{
			return await this.testProcessor.PerformCall(false);
		}
	}
}