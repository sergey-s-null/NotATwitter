namespace Server.Responses;

public record UserInfoResponse(
	string Name,
	string DisplayName,
	string AboutMe
);