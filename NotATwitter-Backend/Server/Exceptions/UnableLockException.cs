namespace Server.Exceptions;

public class UnableLockException : Exception
{
	public UnableLockException(string? message) : base(message)
	{
	}

	public UnableLockException(string? message, Exception? innerException) : base(message, innerException)
	{
	}
}