using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
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
	public void Create(CreateMessageRequest request)
	{
		_messageMongoRepository.Create(new MessageMongoModel(
			ObjectId.Empty,
			ObjectId.Empty, // todo user valid id,
			DateTime.Now,
			request.Text
		));
	}
}