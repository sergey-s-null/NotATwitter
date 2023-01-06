using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.Extensions;
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
	public Task<ActionResult> UpdateInfoAsync()
	{
		throw new NotImplementedException();
	}
}