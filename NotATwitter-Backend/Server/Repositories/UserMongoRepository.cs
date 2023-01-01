using System.Linq.Expressions;
using MongoDB.Driver;
using Server.Models.Mongo;
using Server.Services.Abstract;

namespace Server.Repositories;

public class UserMongoRepository
{
	private readonly ILogger<UserMongoRepository> _logger;
	private readonly IMongoDbCollectionsProvider _mongoDbCollectionsProvider;

	public UserMongoRepository(
		ILogger<UserMongoRepository> logger,
		IMongoDbCollectionsProvider mongoDbCollectionsProvider)
	{
		_logger = logger;
		_mongoDbCollectionsProvider = mongoDbCollectionsProvider;
	}

	public async Task<UserMongoModel?> FindByNameAsync(string name)
	{
		var userCollection = _mongoDbCollectionsProvider.GetUserCollection();

		var user = await userCollection
			.Find(GetNameEqualityFilter(name))
			.FirstOrDefaultAsync();

		return user;
	}

	public async Task<bool> ExistsAsync(string name)
	{
		var userCollection = _mongoDbCollectionsProvider.GetUserCollection();

		var count = await userCollection
			.CountDocumentsAsync(GetNameEqualityFilter(name));

		if (count > 1)
		{
			_logger.LogWarning($"Found more than one user with name \"{name}\".");
		}

		return count >= 1;
	}

	public async Task<UserMongoModel> CreateAsync(UserMongoModel user)
	{
		var userCollection = _mongoDbCollectionsProvider.GetUserCollection();

		await userCollection.InsertOneAsync(user);

		return user;
	}

	private static Expression<Func<UserMongoModel, bool>> GetNameEqualityFilter(string name)
	{
		var nameLower = name.ToLower();
		return x => x.Name.ToLower() == nameLower;
	}
}