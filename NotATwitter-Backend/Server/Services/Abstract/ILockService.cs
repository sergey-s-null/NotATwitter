using Server.Exceptions;

namespace Server.Services.Abstract;

public interface ILockService
{
	/// <param name="name">Name of user</param>
	/// <param name="waitForLock">
	/// True - execution thread will take control until lock has been got.<br/>
	/// False - if lock could not be got immediately, then exception will be thrown.
	/// </param>
	/// <returns>IDisposable object that represent lock itself. It must be disposed to unlock user.</returns>
	/// <exception cref="UnableLockException">Could not get lock immediately.</exception>
	Task<IAsyncDisposable> LockUserAsync(string name, bool waitForLock);

	/// <param name="name">Name of user</param>
	/// <param name="timeout">
	/// If lock didn't get before timeout, then exception will be thrown.
	/// </param>
	/// <returns>IDisposable object that represent lock itself. It must be disposed to unlock user.</returns>
	/// <exception cref="UnableLockException">Could not get lock before timeout.</exception>
	Task<IAsyncDisposable> LockUserAsync(string name, TimeSpan timeout);
}