namespace QuizApp.Application.Services
{
	public interface IPasswordsService : IService
	{
		byte[] GenerateSalt();
		string HashPassword(string password, byte[] salt);
	}
}