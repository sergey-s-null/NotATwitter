using MongoDB.Driver;
using Server.Entities.Abstract;
using Server.Models;
using Server.Services.Abstract;

namespace Server.Services;

public class MongoDbCollectionsProvider : IMongoDbCollectionsProvider
{
	private readonly IApplicationConfiguration _applicationConfiguration;

	public MongoDbCollectionsProvider(IApplicationConfiguration applicationConfiguration)
	{
		_applicationConfiguration = applicationConfiguration;
	}

	public IMongoCollection<UserModel> GetUserCollection()
	{
		var database = GetDatabase();
		return database.GetCollection<UserModel>(_applicationConfiguration.UserCollectionName);
	}

	public IMongoCollection<MessageModel> GetMessageCollection()
	{
		var database = GetDatabase();
		return database.GetCollection<MessageModel>(_applicationConfiguration.MessageCollectionName);
	}

	private IMongoDatabase GetDatabase()
	{
		var client = new MongoClient(_applicationConfiguration.MongoDbConnectionString);
		return client.GetDatabase(_applicationConfiguration.DatabaseName);
	}
}