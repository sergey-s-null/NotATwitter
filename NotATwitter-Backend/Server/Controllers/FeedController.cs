using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class FeedController : ControllerBase
{
	[HttpPost]
	public Task<ActionResult> GetAsync()
	{
		// todo user ES
		throw new NotImplementedException();
	}
}