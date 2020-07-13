using QuizApp.Application.Dtos;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class IdentitiesService : IIdentitiesService
	{
		private readonly IUsersRepository _usersRepository;
		private readonly IPasswordsService _passwordService;
		private readonly ITokensService _tokensService;
		public IdentitiesService(
			IUsersRepository usersRepository,
			IPasswordsService passwordService,
			ITokensService tokensService)
		{
			_usersRepository = usersRepository;
			_passwordService = passwordService;
			_tokensService = tokensService;
		}

		public async Task SignUpAsync(SignUpDto signUpDto)
		{
			var salt = _passwordService.GenerateSalt();
			var password = Password.Create(signUpDto.Password);
			var passwordHash = _passwordService.HashPassword(password.Value, salt);

			if (await _usersRepository.CheckIfExistsByUsernameAsync(signUpDto.Username))
				throw new UserWithSelectedUsernameAlreadyExistsException(signUpDto.Username);

			var user = User.Create(signUpDto.Username, passwordHash, salt);
			await _usersRepository.AddAsync(user);
		}

		public async Task<TokenDto> SignInAsync(SignInDto signInDto)
		{
			var user = await _usersRepository.GetByUsernameAsync(signInDto.Username);
			if (user is null)
				throw new UserNotFoundException(signInDto.Username);

			var passwordHash = _passwordService.HashPassword(signInDto.Password, user.Salt);
			if (passwordHash != user.PasswordHash)
				throw new UserNotFoundException(signInDto.Username);

			return _tokensService.Create(user.Username);
		}

	}
}
