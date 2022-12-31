using MongoDB.Driver;
using Server.Models;

namespace Server.Services.Abstract;

public interface IMongoDbCollectionsProvider
{
	IMongoCollection<UserModel> GetUserCollection();

	IMongoCollection<MessageModel> GetMessageCollection();
}