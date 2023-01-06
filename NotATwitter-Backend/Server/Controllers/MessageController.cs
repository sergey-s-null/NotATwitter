using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.Extensions;
using Server.Models.Mongo;
using Server.Repositories;
using Server.Requests;

namespace Server.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class MessageController : ControllerBase
{
	private readonly MessageMongoRepository _messageMongoRepository;

	public MessageController(MessageMongoRepository messageMongoRepository)
	{
		_messageMongoRepository = messageMongoRepository;
	}

	[HttpPost]
	[Authorize]
	public async Task<ActionResult> Create(CreateMessageRequest request)
	{
		var userId = User.GetMongoDbIdOrDefault();
		if (userId is null)
		{
			return Unauthorized();
		}

		await _messageMongoRepository.CreateAsync(new MessageMongoModel(
			ObjectId.Empty,
			userId.Value,
			request.Title,
			request.Body,
			DateTime.Now,
			DateTime.Now
		));

		return Ok();
	}
}