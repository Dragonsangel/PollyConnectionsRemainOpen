using Microsoft.AspNetCore.Mvc;
using System;

namespace PollyConnectionsRemainOpen.Controllers
{
	[ApiController]
	[Route("/failingroute")]
	public class FailingController : ControllerBase
	{
		[HttpGet("fail")]
		public ActionResult<Boolean> FailThisCall()
		{
			return base.Problem("Failed");
		}
	}
}