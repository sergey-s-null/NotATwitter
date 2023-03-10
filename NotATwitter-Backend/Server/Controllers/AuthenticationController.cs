using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.Entities;
using Server.Enums;
using Server.Exceptions;
using Server.Models.Mongo;
using Server.Repositories;
using Server.Requests;
using Server.Services.Abstract;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class AuthenticationController : ControllerBase
{
	private readonly ILockService _lockService;
	private readonly UserMongoRepository _userMongoRepository;
	private readonly IUserPasswordHashingService _userPasswordHashingService;

	public AuthenticationController(
		ILockService lockService,
		UserMongoRepository userMongoRepository,
		IUserPasswordHashingService userPasswordHashingService)
	{
		_lockService = lockService;
		_userMongoRepository = userMongoRepository;
		_userPasswordHashingService = userPasswordHashingService;
	}

	[HttpPost]
	public async Task<ActionResult> Login(LoginRequest request)
	{
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
	public async Task<ActionResult> Logout()
	{
		await HttpContext.SignOutAsync();

		return Ok();
	}

	[HttpPost]
	public async Task<ActionResult> RegisterAsync(RegisterRequest request)
	{
		UserMongoModel user;
		try
		{
			await using var _ = await _lockService.LockUserAsync(request.Name, false);

			if (await _userMongoRepository.ExistsAsync(request.Name))
			{
				return Conflict("User with the same name already exists.");
			}

			user = CreateUserModel(request);

			await _userMongoRepository.CreateAsync(user);
		}
		catch (UnableLockException e)
		{
			if (e.Reason == UnableLockReason.AlreadyLocked)
			{
				return Conflict("User with the same name already exists.");
			}

			if (e.Reason == UnableLockReason.HazelcastConnectionProblem)
			{
				throw;
			}

			throw new ArgumentOutOfRangeException(nameof(e.Reason), e.Reason, "Unknown reason value.");
		}

		await AuthorizeAsync(user);

		return Ok();
	}

	private UserMongoModel CreateUserModel(RegisterRequest request)
	{
		var passwordHash = _userPasswordHashingService.GetPasswordHash(request.Password);

		return new UserMongoModel(
			ObjectId.Empty,
			request.Name,
			passwordHash,
			request.Name,
			string.Empty
		);
	}

	private async Task AuthorizeAsync(UserMongoModel user)
	{
		var claims = new[]
		{
			new Claim(CustomClaimTypes.MongoDbIdentifier, user.Id.ToString()),
			new Claim(ClaimTypes.Name, user.Name)
		};
		var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

		await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
	}
}