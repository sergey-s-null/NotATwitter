using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Server.Repositories;
using Server.Requests;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
	private readonly UserMongoRepository _userMongoRepository;

	public AuthenticationController(UserMongoRepository userMongoRepository)
	{
		_userMongoRepository = userMongoRepository;
	}

	[HttpPost]
	public async Task<ActionResult> Login(LoginRequest request)
	{
		// todo change authorization later
		var user = await _userMongoRepository.FindByNameAsync(request.Username);

		if (user is null)
		{
			return Unauthorized();
		}

		var claims = new[] { new Claim(ClaimTypes.Name, user.Name) };
		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
		return Ok();
	}
}