using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Server.Requests;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
	[HttpPost]
	public async Task<ActionResult> Login(LoginRequest request)
	{
		// todo remake authorization
		if (request.Password != "123")
		{
			return Unauthorized();
		}
		
		var claims = new[] { new Claim(ClaimTypes.Name, request.Username) };
		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
		return Ok();
	}
}