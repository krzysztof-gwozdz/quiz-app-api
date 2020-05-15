using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using System;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class QuestionSetTests
	{
		
		[Fact]
		public void CreateQuestionSetWithCorrectValues()
		{
			//arrange
			string name = "test name";
			string iconUrl = "test imageUrl";
			string color = "test color";

			//act
			var questionSet = QuestionSet.Create(name, iconUrl, color);

			//assert
			questionSet.Id.Should().NotBeEmpty();
			questionSet.Name.Should().Be(name);
			questionSet.IconUrl.Should().Be(iconUrl);
			questionSet.Color.Should().Be(color);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateQuestionSetWithEmptyName_ThrowException(string name)
		{
			//arrange
			string iconUrl = "test imageUrl";
			string color = "test color";

			//act
			Action createQuestionSet = () => QuestionSet.Create(name, iconUrl, color);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetNameException>()
				.WithMessage("Question set name can not be empty.");
		}
	}
}
