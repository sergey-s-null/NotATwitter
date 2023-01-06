using MongoDB.Bson;
using Server.Models.Mongo;

namespace Server.ReadModels.Mongo;

/// <param name="Id">
/// Name contract with <see cref="UserMongoModel"/>.<see cref="UserMongoModel.Id"/>
/// </param>
/// <param name="DisplayName">
/// Name contract with <see cref="UserMongoModel"/>.<see cref="UserMongoModel.DisplayName"/>
/// </param>
/// <param name="AboutMe">
/// Name contract with <see cref="UserMongoModel"/>.<see cref="UserMongoModel.AboutMe"/>
/// </param>
public record UserPublicInfoMongoModel(
	ObjectId Id,
	string DisplayName,
	string AboutMe
);