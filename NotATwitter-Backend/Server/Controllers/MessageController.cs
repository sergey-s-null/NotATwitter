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
	public ActionResult Create(CreateMessageRequest request)
	{
		var userId = User.GetMongoDbIdOrDefault();
		if (userId is null)
		{
			return Unauthorized();
		}

		_messageMongoRepository.Create(new MessageMongoModel(
			ObjectId.Empty,
			userId.Value,
			DateTime.Now,
			request.Text
		));

		return Ok();
	}
}