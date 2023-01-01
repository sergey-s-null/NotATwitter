using MongoDB.Driver;
using Server.Models.Mongo;

namespace Server.Services.Abstract;

public interface IMongoDbCollectionsProvider
{
	IMongoCollection<UserMongoModel> GetUserCollection();

	IMongoCollection<MessageMongoModel> GetMessageCollection();
}