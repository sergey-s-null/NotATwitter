using Hazelcast;
using Server.Entities.Abstract;
using Server.Exceptions;
using Server.Services.Abstract;

namespace Server.Services;

public sealed class HazelcastClientProvider : IHazelcastClientProvider, IAsyncDisposable
{
	private readonly IHazelcastConfiguration _hazelcastConfiguration;

	private IHazelcastClient? _client;

	public HazelcastClientProvider(IHazelcastConfiguration hazelcastConfiguration)
	{
		_hazelcastConfiguration = hazelcastConfiguration;
	}

	public async ValueTask<IHazelcastClient> GetClientAsync()
	{
		return _client ??= await CreateClientAsync();
	}

	private async ValueTask<IHazelcastClient> CreateClientAsync()
	{
		var options = new HazelcastOptionsBuilder()
			.With(x =>
			{
				foreach (var address in _hazelcastConfiguration.Addresses)
				{
					x.Networking.Addresses.Add(address);
				}
			})
			.With(x => x.ClusterName = _hazelcastConfiguration.ClusterName)
			.Build();

		var tokenSource = new CancellationTokenSource(_hazelcastConfiguration.ConnectionTimeout);
		try
		{
			return await HazelcastClientFactory.StartNewClientAsync(options, tokenSource.Token);
		}
		catch (OperationCanceledException e)
		{
			throw new HazelcastClientCreationException("Could not connect to Hazelcast cluster. Timeout exceeded.", e);
		}
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