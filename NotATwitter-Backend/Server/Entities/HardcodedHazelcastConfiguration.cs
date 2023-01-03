using Server.Entities.Abstract;

namespace Server.Entities;

public class HardcodedHazelcastConfiguration : IHazelcastConfiguration
{
	public IReadOnlyList<string> Addresses { get; } = new[]
	{
		"127.0.0.1:5701"
	};

	public string UserLockMapName => "user-lock";
	public TimeSpan LockCheckPeriod { get; } = TimeSpan.FromMilliseconds(50);
}