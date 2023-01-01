using System.Linq.Expressions;
using MongoDB.Driver;
using Server.Models;
using Server.Services.Abstract;

namespace Server.Repositories;

public class UserMongoRepository
{
	private readonly IMongoDbCollectionsProvider _mongoDbCollectionsProvider;

	public UserMongoRepository(IMongoDbCollectionsProvider mongoDbCollectionsProvider)
	{
		_mongoDbCollectionsProvider = mongoDbCollectionsProvider;
	}

	public async Task<UserModel?> FindByNameAsync(string name)
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

		// todo check and log if count > 1

		return count >= 1;
	}

	public Task CreateAsync(UserModel user)
	{
		var userCollection = _mongoDbCollectionsProvider.GetUserCollection();

		return userCollection.InsertOneAsync(user);
	}

	private static Expression<Func<UserModel, bool>> GetNameEqualityFilter(string name)
	{
		var nameLower = name.ToLower();
		return x => x.Name.ToLower() == nameLower;
	}
}