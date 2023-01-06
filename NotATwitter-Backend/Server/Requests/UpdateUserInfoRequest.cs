namespace Server.Requests;

public record UpdateUserInfoRequest(
	string? DisplayName,
	string? AboutMe
);