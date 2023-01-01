namespace Server.Services.Abstract;

public interface IUserPasswordHashingService
{
	string GetPasswordHash(string password);
}