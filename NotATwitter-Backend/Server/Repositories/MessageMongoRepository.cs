using MongoDB.Bson;
using MongoDB.Driver;
using Server.Models.Mongo;
using Server.Services.Abstract;

namespace Server.Repositories;

public class MessageMongoRepository
{
	private readonly IMongoDbCollectionsProvider _mongoDbCollectionsProvider;

	public MessageMongoRepository(IMongoDbCollectionsProvider mongoDbCollectionsProvider)
	{
		_mongoDbCollectionsProvider = mongoDbCollectionsProvider;
	}

	public async Task<MessageMongoModel> CreateAsync(MessageMongoModel message)
	{
		var messageCollection = _mongoDbCollectionsProvider.GetMessageCollection();

		await messageCollection.InsertOneAsync(message);

		return message;
	}

	public async Task UpdateAsync(MessageMongoModel message)
	{
		var messageCollection = _mongoDbCollectionsProvider.GetMessageCollection();

		await messageCollection.UpdateOneAsync(
			x => x.Id == message.Id,
			Builders<MessageMongoModel>.Update
				.Set(x => x.Title, message.Title)
				.Set(x => x.Body, message.Body)
				.Set(x => x.LastEdited, message.LastEdited)
		);
	}

	public async Task<MessageMongoModel?> FindAsync(ObjectId messageId)
	{
		var messageCollection = _mongoDbCollectionsProvider.GetMessageCollection();

		var searchResult = await messageCollection
			.FindAsync(x => x.Id == messageId);
		return searchResult.FirstOrDefault();
	}
}