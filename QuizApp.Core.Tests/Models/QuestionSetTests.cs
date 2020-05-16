using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
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
			var name = QuestionSetExample.ValidName;
			var iconUrl = QuestionSetExample.ValidIconUrl;
			var color = QuestionSetExample.ValidColor;

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
			var iconUrl = QuestionSetExample.ValidIconUrl;
			var color = QuestionSetExample.ValidColor;

			//act
			Action createQuestionSet = () => QuestionSet.Create(name, iconUrl, color);

			//assert
			createQuestionSet.Should().Throw<EmptyQuestionSetNameException>()
				.WithMessage("Question set name can not be empty.");
		}
	}
}
