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
		private readonly ITokensService _tokensService;
		private readonly IdentitiesService _identitiesService;

		public IdentitiesServiceTests()
		{
			_usersRepository = Substitute.For<IUsersRepository>();
			_passwordService = Substitute.For<IPasswordsService>();
			_tokensService = Substitute.For<ITokensService>();
			_identitiesService = new IdentitiesService(_usersRepository, _passwordService, _tokensService);
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
		}

		[Fact]
		public void SignUpWithUsernameOfAnotherUser_ThrowException()
		{
			//arrange
			var username = UserExample.ValidUsername;
			var dto = new SignUpDto(username, PasswordExample.ValidPassword.Value);
			_usersRepository.CheckIfExistsByUsernameAsync(username).Returns(true);

			//act 
			Func<Task> signUp = async () => await _identitiesService.SignUpAsync(dto);

			//assert
			signUp.Should().Throw<UserWithSelectedUsernameAlreadyExistsException>()
				.WithMessage($"User with username: {username} already exists.");
		}

		[Fact]
		public async Task SignIpWithCorrectCredentials_Token()
		{
			//arrange
			var user = UserExample.ValidUser;
			var dto = new SignInDto(user.Username, PasswordExample.ValidPassword.Value);
			_passwordService.HashPassword(PasswordExample.ValidPassword.Value, Arg.Any<byte[]>()).Returns(UserExample.ValidPasswordHash);
			_usersRepository.GetByUsernameAsync(user.Username).Returns(user);
			_tokensService.Create(user.Username).Returns(Substitute.For<TokenDto>());

			//act 
			var token = await _identitiesService.SignInAsync(dto);

			//assert
			token.Should().NotBeNull();
		}

		[Fact]
		public void SignInWithUsernameOfUserThatDoesNotExist_ThrowException()
		{
			//arrange
			var user = UserExample.ValidUser;
			var dto = new SignInDto(user.Username, PasswordExample.ValidPassword.Value);
			_passwordService.HashPassword(PasswordExample.ValidPassword.Value, Arg.Any<byte[]>()).Returns(UserExample.ValidPasswordHash);

			//act 
			Func<Task> signIn = async () => await _identitiesService.SignInAsync(dto);

			//assert
			signIn.Should().Throw<UserNotFoundException>()
				.WithMessage($"User: {user.Username} not found.");
		}

		[Fact]
		public void SignInWithValidUsernameButWrongPassword_ThrowException()
		{
			//arrange
			var user = UserExample.ValidUser;
			var dto = new SignInDto(user.Username, "wrong password");
			_usersRepository.GetByUsernameAsync(user.Username).Returns(user);

			//act 
			Func<Task> signIn = async () => await _identitiesService.SignInAsync(dto);

			//assert
			signIn.Should().Throw<UserNotFoundException>()
				.WithMessage($"User: {user.Username} not found.");
		}
	}
}
