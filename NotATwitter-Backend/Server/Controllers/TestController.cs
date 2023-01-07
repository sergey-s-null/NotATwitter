using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
	[HttpGet]
	public async Task<ActionResult<string>> Get()
	{
		return "Hi, I'm working!";
	}
}