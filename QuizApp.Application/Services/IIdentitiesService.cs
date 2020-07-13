using QuizApp.Application.Dtos;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IIdentitiesService : IService
	{
		Task SignUpAsync(SignUpDto signUpDto);
		Task<TokenDto> SignInAsync(SignInDto signInDto);
	}
}
