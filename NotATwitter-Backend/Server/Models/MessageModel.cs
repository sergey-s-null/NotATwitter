using MongoDB.Bson;

namespace Server.Models;

public class MessageModel
{
	public ObjectId Id { get; set; }
	public string Text { get; set; } = string.Empty;
}