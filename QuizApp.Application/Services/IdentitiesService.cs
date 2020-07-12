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

		public IdentitiesService(
			IIdentitiesRepository identitiesRepository,
			IPasswordsService passwordService)
		{
			_identitiesRepository = identitiesRepository;
			_passwordService = passwordService;
		}

		public async Task SignUpAsync(SignUpDto signUpDto)
		{
			var salt = _passwordService.GenerateSalt();
			var password = Password.Create(signUpDto.Password);
			var passwordHash = _passwordService.HashPassword(password, salt);

			if (await _identitiesRepository.ExistsAsync(signUpDto.Username))
				throw new IdentityWithSelectedUsernameAlreadyExistsException(signUpDto.Username);

			var identity = Identity.Create(signUpDto.Username, passwordHash, salt);
			await _identitiesRepository.AddAsync(identity);
		}
	}
}
