using MongoDB.Driver;
using Server.Entities.Abstract;
using Server.Models.Mongo;
using Server.Services.Abstract;

namespace Server.Services;

public class MongoDbCollectionsProvider : IMongoDbCollectionsProvider
{
	private readonly IApplicationConfiguration _applicationConfiguration;

	public MongoDbCollectionsProvider(IApplicationConfiguration applicationConfiguration)
	{
		_applicationConfiguration = applicationConfiguration;
	}

	public IMongoCollection<UserMongoModel> GetUserCollection()
	{
		var database = GetDatabase();
		return database.GetCollection<UserMongoModel>(_applicationConfiguration.UserCollectionName);
	}

	public IMongoCollection<MessageMongoModel> GetMessageCollection()
	{
		var database = GetDatabase();
		return database.GetCollection<MessageMongoModel>(_applicationConfiguration.MessageCollectionName);
	}

	private IMongoDatabase GetDatabase()
	{
		var client = new MongoClient(_applicationConfiguration.MongoDbConnectionString);
		return client.GetDatabase(_applicationConfiguration.DatabaseName);
	}
}