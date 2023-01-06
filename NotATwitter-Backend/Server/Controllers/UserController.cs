using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.Extensions;
using Server.Models.Mongo;
using Server.Repositories;
using Server.Requests;
using Server.Responses;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
	private readonly ILogger<UserController> _logger;
	private readonly IMapper _mapper;
	private readonly UserMongoRepository _userMongoRepository;

	public UserController(
		ILogger<UserController> logger,
		IMapper mapper,
		UserMongoRepository userMongoRepository)
	{
		_logger = logger;
		_mapper = mapper;
		_userMongoRepository = userMongoRepository;
	}

	[HttpPost]
	public async Task<ActionResult<UserPublicInfoResponse>> GetPublicInfoAsync(GetUserPublicInfoRequest request)
	{
		if (!ObjectId.TryParse(request.UserId, out var userId))
		{
			return BadRequest();
		}

		// todo use ES instead
		var publicInfo = await _userMongoRepository.FindPublicInfoAsync(userId);
		if (publicInfo is null)
		{
			return NotFound();
		}

		// todo add mappings
		var response = _mapper.Map<UserPublicInfoResponse>(publicInfo);
		return response;
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult<UserInfoResponse>> GetInfoAsync()
	{
		var userId = User.GetMongoDbIdOrNull();
		if (userId is null)
		{
			return Unauthorized();
		}

		var user = await _userMongoRepository.FindAsync(userId.Value);
		if (user is null)
		{
			_logger.LogError($"User with id {userId} not found in database, but it's already authorized.");
			return BadRequest();
		}

		// todo add mapping
		var response = _mapper.Map<UserInfoResponse>(user);
		return response;
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult> UpdateInfoAsync(UpdateUserInfoRequest request)
	{
		// todo lock user via hazelcast
		var userId = User.GetMongoDbIdOrNull();
		if (userId is null)
		{
			return Unauthorized();
		}

		var user = await _userMongoRepository.FindAsync(userId.Value);
		if (user is null)
		{
			_logger.LogError($"User with id {userId} not found in database, but it's already authorized.");
			return BadRequest();
		}

		var changedUser = ApplyChanges(user, request);
		await _userMongoRepository.UpdateAsync(changedUser);

		return Ok();
	}

	private static UserMongoModel ApplyChanges(UserMongoModel user, UpdateUserInfoRequest request)
	{
		return user with
		{
			DisplayName = request.DisplayName ?? user.DisplayName,
			AboutMe = request.AboutMe ?? user.AboutMe
		};
	}
}