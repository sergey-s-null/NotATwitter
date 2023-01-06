using System.Linq.Expressions;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using Server.Models.Mongo;
using Server.ReadModels.Mongo;
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

	public async Task<UserMongoModel?> FindAsync(ObjectId id)
	{
		var userCollection = _mongoDbCollectionsProvider.GetUserCollection();

		var user = await userCollection
			.Find(x => x.Id == id)
			.FirstOrDefaultAsync();

		return user;
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

	public async Task<UserPublicInfoMongoModel?> FindPublicInfoAsync(ObjectId userId)
	{
		var userCollection = _mongoDbCollectionsProvider.GetUserCollection();

		var document = await userCollection
			.Find(x => x.Id == userId)
			.Project(
				Builders<UserMongoModel>.Projection
					.Include(x => x.Id)
					.Include(x => x.DisplayName)
					.Include(x => x.AboutMe)
			)
			.FirstOrDefaultAsync();

		return document is not null
			? BsonSerializer.Deserialize<UserPublicInfoMongoModel>(document)
			: null;
	}

	public async Task<UserMongoModel> CreateAsync(UserMongoModel user)
	{
		var userCollection = _mongoDbCollectionsProvider.GetUserCollection();

		await userCollection.InsertOneAsync(user);

		return user;
	}

	public async Task UpdateAsync(UserMongoModel user)
	{
		var userCollection = _mongoDbCollectionsProvider.GetUserCollection();

		await userCollection.UpdateOneAsync(
			x => x.Id == user.Id,
			Builders<UserMongoModel>.Update
				.Set(x => x.PasswordHash, user.PasswordHash)
				.Set(x => x.DisplayName, user.DisplayName)
				.Set(x => x.AboutMe, user.AboutMe)
		);
	}

	private static Expression<Func<UserMongoModel, bool>> GetNameEqualityFilter(string name)
	{
		var nameLower = name.ToLower();
		return x => x.Name.ToLower() == nameLower;
	}
}