namespace Server.Entities.Abstract;

public interface IApplicationConfiguration
{
	string MongoDbConnectionString { get; }
	string DatabaseName { get; }
	string UserCollectionName { get; }
	string MessageCollectionName { get; }
}