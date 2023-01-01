using MongoDB.Bson;

namespace Server.Models.Mongo;

public record UserMongoModel(
	ObjectId Id,
	string Name,
	string PasswordHash
);