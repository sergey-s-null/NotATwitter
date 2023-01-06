using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
	[HttpPost]
	public Task<ActionResult> GetPublicInfoAsync()
	{
		throw new NotImplementedException();
	}

	[HttpPost]
	[Authorize]
	public Task<ActionResult> GetInfoAsync()
	{
		throw new NotImplementedException();
	}

	[HttpPost]
	[Authorize]
	public Task<ActionResult> UpdateInfoAsync()
	{
		throw new NotImplementedException();
	}
}