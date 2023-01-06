namespace Server.Requests;

public record UpdateMessageRequest(
	string MessageId,
	string? Title,
	string? Body
);