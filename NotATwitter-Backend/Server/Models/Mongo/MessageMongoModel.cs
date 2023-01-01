using MongoDB.Bson;

namespace Server.Models.Mongo;

public record MessageMongoModel(
	ObjectId Id,
	ObjectId AuthorId,
	DateTime Created,
	string Text
);