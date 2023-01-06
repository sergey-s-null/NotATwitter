namespace Server.Requests;

public record CreateMessageRequest(
	string Title,
	string Body
);