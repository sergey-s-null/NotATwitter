using Hazelcast;
using Server.Exceptions;

namespace Server.Services.Abstract;

public interface IHazelcastClientProvider
{
	/// <exception cref="HazelcastClientCreationException">Could connect to Hazelcast.</exception>
	ValueTask<IHazelcastClient> GetClientAsync();
}