using Server.Models;
using Server.Services.Abstract;

namespace Server.Repositories;

public class MessageMongoRepository
{
	private readonly IMongoDbCollectionsProvider _mongoDbCollectionsProvider;

	public MessageMongoRepository(IMongoDbCollectionsProvider mongoDbCollectionsProvider)
	{
		_mongoDbCollectionsProvider = mongoDbCollectionsProvider;
	}

	public void Create(MessageModel message)
	{
		var messageCollection = _mongoDbCollectionsProvider.GetMessageCollection();
		messageCollection.InsertOne(message);
	}
}