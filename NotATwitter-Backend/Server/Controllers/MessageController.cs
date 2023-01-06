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
	public async Task<ActionResult> CreateAsync(CreateMessageRequest request)
	{
		var userId = User.GetMongoDbIdOrNull();
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

	[HttpPost]
	[Authorize]
	public async Task<ActionResult> UpdateAsync(UpdateMessageRequest request)
	{
		var userId = User.GetMongoDbIdOrNull();
		if (userId is null)
		{
			return Unauthorized();
		}

		if (!ObjectId.TryParse(request.MessageId, out var messageId))
		{
			return BadRequest();
		}

		var message = await _messageMongoRepository.FindAsync(messageId);
		if (message is null)
		{
			return NotFound();
		}

		if (message.AuthorId != userId)
		{
			return Forbid();
		}

		var changedMessage = ApplyChanges(message, request);
		await _messageMongoRepository.UpdateAsync(changedMessage);

		return Ok();
	}

	[HttpPost]
	[Authorize]
	public Task<ActionResult> DeleteAsync()
	{
		throw new NotImplementedException();
	}

	private static MessageMongoModel ApplyChanges(MessageMongoModel message, UpdateMessageRequest request)
	{
		return message with
		{
			Title = request.Title ?? message.Title,
			Body = request.Body ?? message.Body,
			LastEdited = DateTime.Now
		};
	}
}