using QuizApp.Application.Dtos;

namespace QuizApp.Application.Services
{
	public interface ITokensService : IService
	{
		TokenDto Create(string username);
	}
}
