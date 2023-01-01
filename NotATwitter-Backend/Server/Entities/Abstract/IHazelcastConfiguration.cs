namespace Server.Entities.Abstract;

public interface IHazelcastConfiguration
{
	string UserLockMapName { get; }
	TimeSpan LockCheckPeriod { get; }
}