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
		private readonly IIdentitiesRepository _identitiesRepository;
		private readonly IPasswordsService _passwordService;
		private readonly ITokensService _tokenService;
		private readonly IdentitiesService _identitiesService;

		public IdentitiesServiceTests()
		{
			_identitiesRepository = Substitute.For<IIdentitiesRepository>();
			_passwordService = Substitute.For<IPasswordsService>();
			_tokenService = Substitute.For<ITokensService>();
			_identitiesService = new IdentitiesService(_identitiesRepository, _passwordService, _tokenService);
		}

		[Fact]
		public async Task SignUpWithUniqueUsername_IdentityCreated()
		{
			//arrange
			var dto = new SignUpDto(IdentityExample.ValidUsername, PasswordExample.ValidPassword.Value);
			_passwordService.GenerateSalt().Returns(IdentityExample.ValidSalt);
			_passwordService.HashPassword(Arg.Any<string>(), Arg.Any<byte[]>()).Returns(IdentityExample.ValidPasswordHash);

			//act 
			await _identitiesService.SignUpAsync(dto);

			//assert
			//TODO check if identity was created.
		}

		[Fact]
		public void SignUpWithUsernameNameOfAnotherIdentity_ThrowException()
		{
			//arrange
			var username = IdentityExample.ValidUsername;
			var dto = new SignUpDto(username, PasswordExample.ValidPassword.Value);
			_identitiesRepository.CheckIfExistsByUsernameAsync(Arg.Any<string>()).Returns(true);

			//act 
			Func<Task> signUp = async () => await _identitiesService.SignUpAsync(dto);

			//assert
			signUp.Should().Throw<IdentityWithSelectedUsernameAlreadyExistsException>()
				.WithMessage($"Identity with username: {username} already exists.");
		}
	}
}
