namespace Server.Exceptions;

public class HazelcastClientCreationException : Exception
{
	public HazelcastClientCreationException(string? message) : base(message)
	{
	}

	public HazelcastClientCreationException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}