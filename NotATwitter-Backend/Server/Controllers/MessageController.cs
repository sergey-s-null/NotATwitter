using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Server.Models;
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
		_messageMongoRepository.Create(new MessageModel
		{
			Id = ObjectId.GenerateNewId(),
			Text = request.Text
		});
	}
}