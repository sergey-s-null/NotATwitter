namespace Server.Responses;

public record UserPublicInfoResponse(
	string DisplayInfo,
	string AboutMe
);