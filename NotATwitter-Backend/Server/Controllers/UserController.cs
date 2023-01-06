using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.Repositories;
using Server.Requests;
using Server.Responses;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class UserController : ControllerBase
{
	private readonly IMapper _mapper;
	private readonly UserMongoRepository _userMongoRepository;

	public UserController(
		IMapper mapper,
		UserMongoRepository userMongoRepository)
	{
		_mapper = mapper;
		_userMongoRepository = userMongoRepository;
	}

	[HttpPost]
	public async Task<ActionResult<UserPublicInfoResponse>> GetPublicInfoAsync(UserPublicInfoRequest request)
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

		return Ok(response);
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