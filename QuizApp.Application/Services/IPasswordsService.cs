using QuizApp.Core.Models;

namespace QuizApp.Application.Services
{
	public interface IPasswordsService : IService
	{
		byte[] GenerateSalt();
		string HashPassword(Password password, byte[] salt);
	}
}