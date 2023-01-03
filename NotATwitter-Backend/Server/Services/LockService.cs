using Hazelcast;
using Hazelcast.DistributedObjects;
using Server.Entities.Abstract;
using Server.Enums;
using Server.Exceptions;
using Server.Services.Abstract;

namespace Server.Services;

public class LockService : ILockService
{
	private readonly IHazelcastConfiguration _hazelcastConfiguration;
	private readonly IHazelcastClientProvider _hazelcastClientProvider;

	public LockService(
		IHazelcastConfiguration hazelcastConfiguration,
		IHazelcastClientProvider hazelcastClientProvider)
	{
		_hazelcastConfiguration = hazelcastConfiguration;
		_hazelcastClientProvider = hazelcastClientProvider;
	}

	public Task<IAsyncDisposable> LockUserAsync(string name, bool waitForLock)
	{
		return LockUserInternalAsync(name, () => !waitForLock);
	}

	public Task<IAsyncDisposable> LockUserAsync(string name, TimeSpan timeout)
	{
		var start = DateTime.Now;
		return LockUserInternalAsync(name, () => DateTime.Now - start > timeout);
	}

	private async Task<IAsyncDisposable> LockUserInternalAsync(string name, Func<bool> isInterrupt)
	{
		var client = await GetClientAsync();

		var map = await client.GetMapAsync<string, bool>(_hazelcastConfiguration.UserLockMapName);

		var key = GetKeyForUserLock(name);

		while (true)
		{
			try
			{
				await map.LockAsync(key);

				var locked = await map.GetAsync(key);
				if (!locked)
				{
					await map.SetAsync(key, true);
					return new UserLock(map, key);
				}

				if (isInterrupt())
				{
					throw new UnableLockException(
						UnableLockReason.AlreadyLocked,
						$"Could not get lock for user with name \"{name}\"."
					);
				}

				await Task.Delay(_hazelcastConfiguration.LockCheckPeriod);
			}
			finally
			{
				await map.UnlockAsync(key);
			}
		}
	}

	private async Task<IHazelcastClient> GetClientAsync()
	{
		try
		{
			return await _hazelcastClientProvider.GetClientAsync();
		}
		catch (HazelcastClientCreationException e)
		{
			throw new UnableLockException(UnableLockReason.HazelcastConnectionProblem, null, e);
		}
	}

	private static string GetKeyForUserLock(string name)
	{
		return name.ToLower();
	}

	private sealed class UserLock : IAsyncDisposable
	{
		private readonly IHMap<string, bool> _map;
		private readonly string _key;

		public UserLock(IHMap<string, bool> map, string key)
		{
			_map = map;
			_key = key;
		}

		public async ValueTask DisposeAsync()
		{
			try
			{
				await _map.LockAsync(_key);
				await _map.SetAsync(_key, false);
			}
			finally
			{
				await _map.UnlockAsync(_key);
			}

			await _map.DisposeAsync();
		}
	}
}