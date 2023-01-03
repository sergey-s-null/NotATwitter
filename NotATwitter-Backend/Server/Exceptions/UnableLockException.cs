using Server.Enums;

namespace Server.Exceptions;

public class UnableLockException : Exception
{
	public UnableLockReason Reason { get; }

	public UnableLockException(UnableLockReason reason, string? message)
		: base(message ?? reason.Message)
	{
		Reason = reason;
	}

	public UnableLockException(UnableLockReason reason, string? message, Exception? innerException)
		: base(message ?? reason.Message, innerException)
	{
		Reason = reason;
	}
}