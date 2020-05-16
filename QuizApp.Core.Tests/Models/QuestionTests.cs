using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using System;
using System.Linq;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class QuestionTests
	{
		[Fact]
		public void CreateQuestionWithCorrectValues()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.ValidAnswers;
			var correctAnswer = answers.First().Text;
			var questionSetId = QuestionSetExample.NewId;

			//act
			var question = Question.Create(text, answers, correctAnswer, questionSetId);

			//assert
			question.Id.Should().NotBeEmpty();
			question.Text.Should().Be(text);
			question.Answers.Should().BeEquivalentTo(answers);
			question.CorrectAnswerId.Should().Be(answers.First().Id);
			question.QuestionSetId.Should().Be(questionSetId);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateQuestionWithEmptyText_ThrowException(string text)
		{
			//arrange
			var answers = QuestionExample.ValidAnswers;
			var correctAnswer = answers.First().Text;
			var questionSetId = QuestionSetExample.NewId;

			//act
			Action createQuestion = () => Question.Create(text, answers, correctAnswer, questionSetId);

			//assert
			createQuestion.Should().Throw<EmptyQuestionTextException>()
				.WithMessage("Question text can not be empty.");
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public void CreateQuestionWithLessThan2Answers_ThrowException(int numberOfAnswers)
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = Enumerable.Range(0, numberOfAnswers).Select(x => QuestionExample.Answer.ValidAnswer);
			var correctAnswer = Guid.NewGuid().ToString();
			var questionSetId = QuestionSetExample.NewId;

			//act
			Action createQuestion = () => Question.Create(text, answers.ToHashSet(), correctAnswer, questionSetId);

			//assert
			createQuestion.Should().Throw<InvalidNumberOfAnswersInQuestionException>()
				.WithMessage($"Number of answers in question invalid: {numberOfAnswers}.");
		}

		[Fact]
		public void CreateQuestionWithDuplicatedAnswwers_ThrowException()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var duplicatedAnswerText = Guid.NewGuid().ToString();
			var answers = new[]
			{
				QuestionExample.Answer.ValidAnswer,
				new Question.Answer(Guid.NewGuid(), duplicatedAnswerText),
				new Question.Answer(Guid.NewGuid(), duplicatedAnswerText),
				QuestionExample.Answer.ValidAnswer,
			}.ToHashSet();
			var correctAnswer = answers.First().Text;
			var questionSetId = QuestionSetExample.NewId;

			//act
			Action createQuestion = () => Question.Create(text, answers, correctAnswer, questionSetId);

			//assert
			createQuestion.Should().Throw<QuestionContainsDuplicatedAnswersException>()
				.WithMessage($"Question contains duplicated answers: {duplicatedAnswerText}.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateQuestionWithEmptyCorrectAnswer_ThrowException(string correctAnswer)
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.ValidAnswers;
			var questionSetId = QuestionSetExample.NewId;

			//act
			Action createQuestion = () => Question.Create(text, answers, correctAnswer, questionSetId);

			//assert
			createQuestion.Should().Throw<EmptyCorrectAnswerException>()
				.WithMessage("Correct answer can not be empty.");
		}

		[Fact]
		public void CreateQuestionWithAnswerThatIsNotOneOfAnswers_ThrowException()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.ValidAnswers;
			var correctAnswer = QuestionExample.Answer.ValidText;
			var questionSetId = QuestionSetExample.NewId;

			//act
			Action createQuestion = () => Question.Create(text, answers, correctAnswer, questionSetId);

			//assert
			createQuestion.Should().Throw<CorrectAnswerIsNotOneOfAnswersException>()
				.WithMessage($"Correct answer: {correctAnswer} is not one of answers.");
		}

		public class AnswerTests
		{
			[Fact]
			public void CreateAnswerWithCorrectText()
			{
				//arrange
				var text = QuestionExample.Answer.ValidText;

				//act
				var answer = Question.Answer.Create(text);

				//assert
				answer.Id.Should().NotBeEmpty();
				answer.Text.Should().Be(text);
			}

			[Theory]
			[InlineData(null)]
			[InlineData("")]
			[InlineData(" ")]
			public void CreateAnswerWithEmptyText_ThrowException(string text)
			{
				//arrange

				//act
				Action createAnswer = () => Question.Answer.Create(text);

				//assert
				createAnswer.Should().Throw<EmptyAnswerTextException>()
					.WithMessage("Answer text can not be empty.");
			}
		}
	}
}
