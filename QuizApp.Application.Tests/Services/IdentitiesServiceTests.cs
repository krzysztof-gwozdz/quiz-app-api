using FluentAssertions;
using NSubstitute;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Repositories;
using QuizApp.Core.Tests.Examples;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Services
{
	public class IdentitiesServiceTests
	{
		private readonly IUsersRepository _usersRepository;
		private readonly IPasswordsService _passwordService;
		private readonly ITokensService _tokenService;
		private readonly IdentitiesService _identitiesService;

		public IdentitiesServiceTests()
		{
			_usersRepository = Substitute.For<IUsersRepository>();
			_passwordService = Substitute.For<IPasswordsService>();
			_tokenService = Substitute.For<ITokensService>();
			_identitiesService = new IdentitiesService(_usersRepository, _passwordService, _tokenService);
		}

		[Fact]
		public async Task SignUpWithUniqueUsername_UserCreated()
		{
			//arrange
			var dto = new SignUpDto(UserExample.ValidUsername, PasswordExample.ValidPassword.Value);
			_passwordService.GenerateSalt().Returns(UserExample.ValidSalt);
			_passwordService.HashPassword(Arg.Any<string>(), Arg.Any<byte[]>()).Returns(UserExample.ValidPasswordHash);

			//act 
			await _identitiesService.SignUpAsync(dto);

			//assert
			//TODO check if identity was created.
		}

		[Fact]
		public void SignUpWithUsernameNameOfAnotherUser_ThrowException()
		{
			//arrange
			var username = UserExample.ValidUsername;
			var dto = new SignUpDto(username, PasswordExample.ValidPassword.Value);
			_usersRepository.CheckIfExistsByUsernameAsync(Arg.Any<string>()).Returns(true);

			//act 
			Func<Task> signUp = async () => await _identitiesService.SignUpAsync(dto);

			//assert
			signUp.Should().Throw<UserWithSelectedUsernameAlreadyExistsException>()
				.WithMessage($"User with username: {username} already exists.");
		}
	}
}
