using System.Security.Claims;
using MongoDB.Bson;
using Server.Entities;

namespace Server.Extensions;

public static class ClaimsPrincipalExtensions
{
	public static ObjectId? GetMongoDbIdOrDefault(this ClaimsPrincipal claimsPrincipal)
	{
		var idStr = claimsPrincipal.Claims
			.FirstOrDefault(x => x.Type == CustomClaimTypes.MongoDbIdentifier)?
			.Value;

		if (idStr is null)
		{
			return null;
		}

		return ObjectId.TryParse(idStr, out var id)
			? id
			: null;
	}
}