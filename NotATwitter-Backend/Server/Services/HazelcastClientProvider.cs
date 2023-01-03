using Hazelcast;
using Server.Entities.Abstract;
using Server.Services.Abstract;

namespace Server.Services;

public sealed class HazelcastClientProvider : IHazelcastClientProvider, IAsyncDisposable
{
	public IHazelcastClient Client => _client ??= CreateClientAsync().Result;

	private readonly IHazelcastConfiguration _hazelcastConfiguration;

	private IHazelcastClient? _client;

	public HazelcastClientProvider(IHazelcastConfiguration hazelcastConfiguration)
	{
		_hazelcastConfiguration = hazelcastConfiguration;
	}

	private async Task<IHazelcastClient> CreateClientAsync()
	{
		var options = new HazelcastOptionsBuilder()
			.With(x =>
			{
				foreach (var address in _hazelcastConfiguration.Addresses)
				{
					x.Networking.Addresses.Add(address);
				}
			})
			.Build();

		return await HazelcastClientFactory.StartNewClientAsync(options);
	}

	public async ValueTask DisposeAsync()
	{
		if (_client is not null)
		{
			await _client.DisposeAsync();
			_client = null;
		}
	}
}