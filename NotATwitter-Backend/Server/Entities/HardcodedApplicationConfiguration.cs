using Server.Entities.Abstract;

namespace Server.Entities;

public class HardcodedApplicationConfiguration : IApplicationConfiguration
{
	public string MongoDbConnectionString => "mongodb://localhost:27017";
	public string DatabaseName => "Main";
	public string UserCollectionName => "User";
	public string MessageCollectionName => "Message";
}