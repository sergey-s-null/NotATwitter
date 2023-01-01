using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repositories;
using Server.Requests;
using Server.Services.Abstract;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
	private readonly UserMongoRepository _userMongoRepository;
	private readonly IUserPasswordHashingService _userPasswordHashingService;

	public AuthenticationController(
		UserMongoRepository userMongoRepository,
		IUserPasswordHashingService userPasswordHashingService)
	{
		_userMongoRepository = userMongoRepository;
		_userPasswordHashingService = userPasswordHashingService;
	}

	[HttpPost]
	public async Task<ActionResult> Login(LoginRequest request)
	{
		// todo sync account changes with hazelcast
		var user = await _userMongoRepository.FindByNameAsync(request.Username);

		if (user is null)
		{
			return Unauthorized();
		}

		var passwordHash = _userPasswordHashingService.GetPasswordHash(request.Password);
		if (user.PasswordHash != passwordHash)
		{
			return Unauthorized();
		}

		await AuthorizeAsync(user);

		return Ok();
	}

	[HttpPost]
	public async Task<ActionResult> RegisterAsync(RegisterRequest request)
	{
		// todo sync with hazelcast
		if (await _userMongoRepository.ExistsAsync(request.Name))
		{
			return Conflict("User with the same name already exists.");
		}

		var passwordHash = _userPasswordHashingService.GetPasswordHash(request.Password);
		var user = new UserModel(request.Name, passwordHash);
		await _userMongoRepository.CreateAsync(user);

		await AuthorizeAsync(user);

		return Ok();
	}

	private async Task AuthorizeAsync(UserModel user)
	{
		var claims = new[] { new Claim(ClaimTypes.Name, user.Name) };
		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
	}
}