using System.Security.Cryptography;
using System.Text;
using Server.Services.Abstract;

namespace Server.Services;

public class UserPasswordHashingService : IUserPasswordHashingService
{
	public string GetPasswordHash(string password)
	{
		var hashAlgorithm = MD5.Create();
		var bytes = Encoding.UTF8.GetBytes(password);
		return Convert.ToBase64String(hashAlgorithm.ComputeHash(bytes));
	}
}