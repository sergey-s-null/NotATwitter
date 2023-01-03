namespace Server.Entities.Abstract;

public interface IHazelcastConfiguration
{
	IReadOnlyList<string> Addresses { get; }
	TimeSpan ConnectionTimeout { get; }

	string ClusterName { get; }
	string ClientName { get; }

	string UserLockMapName { get; }
	TimeSpan LockCheckPeriod { get; }
}