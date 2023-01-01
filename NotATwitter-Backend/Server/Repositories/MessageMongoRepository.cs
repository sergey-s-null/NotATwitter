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
}