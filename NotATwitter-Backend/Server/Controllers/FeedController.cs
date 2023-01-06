using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FeedController : ControllerBase
{
	[HttpPost]
	public Task<ActionResult> GetAsync()
	{
		throw new NotImplementedException();
	}
}