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
			.Find(x => x.Name == name)
			.FirstOrDefaultAsync();

		return user;
	}
}