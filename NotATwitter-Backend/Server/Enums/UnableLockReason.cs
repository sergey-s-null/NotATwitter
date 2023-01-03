namespace Server.Enums;

public sealed class UnableLockReason
{
	public static readonly UnableLockReason HazelcastConnectionProblem =
		new("Could not establish connection to Hazelcast server.");

	public static readonly UnableLockReason AlreadyLocked = new();
	public string? Message { get; }

	private UnableLockReason(string? message = null)
	{
		Message = message;
	}
}