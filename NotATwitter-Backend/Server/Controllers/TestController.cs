using Microsoft.AspNetCore.Mvc;

namespace Server.Controllers;

[ApiController]
[Route("[controller]")]
public class TestController : ControllerBase
{
	public Task<ActionResult> Get()
	{
		return Task.FromResult<ActionResult>(Ok("Hi, I'm working!"));
	}
}