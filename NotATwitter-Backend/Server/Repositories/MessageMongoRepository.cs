using MongoDB.Driver;
using Server.Models;

namespace Server.Repositories;

public class MessageMongoRepository
{
	public void Create(MessageModel message)
	{
		var messageCollection = GetMessageCollection();
		messageCollection.InsertOne(message);
	}

	private static IMongoCollection<MessageModel> GetMessageCollection()
	{
		var client = new MongoClient("mongodb://localhost:27017");
		var mainDatabase = client.GetDatabase("Main");

		return mainDatabase.GetCollection<MessageModel>("Message");
	}
}