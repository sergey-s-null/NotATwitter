using Hazelcast;

namespace Server.Services.Abstract;

public interface IHazelcastClientProvider
{
	IHazelcastClient Client { get; }
}