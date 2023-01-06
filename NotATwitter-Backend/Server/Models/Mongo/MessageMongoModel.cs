using MongoDB.Bson;

namespace Server.Models.Mongo;

public record MessageMongoModel(
	ObjectId Id,
	ObjectId AuthorId,
	string Title,
	string Body,
	DateTime Created,
	DateTime LastEdited
);