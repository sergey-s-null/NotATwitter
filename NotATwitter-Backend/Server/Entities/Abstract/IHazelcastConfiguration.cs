namespace Server.Entities.Abstract;

public interface IHazelcastConfiguration
{
	IReadOnlyList<string> Addresses { get; }

	string UserLockMapName { get; }
	TimeSpan LockCheckPeriod { get; }
}