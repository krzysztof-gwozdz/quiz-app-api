using QuizApp.Application.Dtos;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class IdentitiesService : IIdentitiesService
	{
		private readonly IIdentitiesRepository _identitiesRepository;
		private readonly IPasswordsService _passwordService;
		private readonly ITokensService _tokensService;
		public IdentitiesService(
			IIdentitiesRepository identitiesRepository,
			IPasswordsService passwordService,
			ITokensService tokensService)
		{
			_identitiesRepository = identitiesRepository;
			_passwordService = passwordService;
			_tokensService = tokensService;
		}

		public async Task SignUpAsync(SignUpDto signUpDto)
		{
			var salt = _passwordService.GenerateSalt();
			var password = Password.Create(signUpDto.Password);
			var passwordHash = _passwordService.HashPassword(password.Value, salt);

			if (await _identitiesRepository.CheckIfExistsByUsernameAsync(signUpDto.Username))
				throw new IdentityWithSelectedUsernameAlreadyExistsException(signUpDto.Username);

			var identity = Identity.Create(signUpDto.Username, passwordHash, salt);
			await _identitiesRepository.AddAsync(identity);
		}

		public async Task<TokenDto> SignInAsync(SignInDto signInDto)
		{
			var identity = await _identitiesRepository.GetByUsernameAsync(signInDto.Username);
			if (identity is null)
				throw new IdentityNotFoundException(signInDto.Username);

			var passwordHash = _passwordService.HashPassword(signInDto.Password, identity.Salt);
			if (passwordHash != identity.PasswordHash)
				throw new IdentityNotFoundException(signInDto.Username);

			return _tokensService.Create(identity.Username);
		}

	}
}
