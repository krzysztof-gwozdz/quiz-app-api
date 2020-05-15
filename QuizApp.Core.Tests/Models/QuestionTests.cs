using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
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
			string text = "test text";
			var answers = new Question.Answer[]
			{
				new Question.Answer(Guid.NewGuid(), "test answer 1"),
				new Question.Answer(Guid.NewGuid(), "test answer 2"),
				new Question.Answer(Guid.NewGuid(), "test answer 3"),
				new Question.Answer(Guid.NewGuid(), "test answer 4"),
			};
			string correctAnswer = answers[0].Text;
			Guid questionSetId = Guid.NewGuid();

			//act
			var question = Question.Create(text, answers.ToHashSet(), correctAnswer, questionSetId);

			//assert
			question.Id.Should().NotBeEmpty();
			question.Text.Should().Be(text);
			question.Answers.Should().BeEquivalentTo(answers);
			question.CorrectAnswerId.Should().Be(answers[0].Id);
			question.QuestionSetId.Should().Be(questionSetId);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateQuestionWithEmptyText_ThrowException(string text)
		{
			//arrange
			var answers = new Question.Answer[]
			{
				new Question.Answer(Guid.NewGuid(), "test answer 1"),
				new Question.Answer(Guid.NewGuid(), "test answer 2"),
				new Question.Answer(Guid.NewGuid(), "test answer 3"),
				new Question.Answer(Guid.NewGuid(), "test answer 4"),
			};
			string correctAnswer = answers[0].Text;
			Guid questionSetId = Guid.NewGuid();

			//act
			Action createQuestion = () => Question.Create(text, answers.ToHashSet(), correctAnswer, questionSetId);

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
			string text = "test text";
			var answers = new List<Question.Answer>();
			for (int i = 0; i < numberOfAnswers; i++)
				answers.Add(new Question.Answer(Guid.NewGuid(), Guid.NewGuid().ToString()));
			string correctAnswer = Guid.NewGuid().ToString();
			Guid questionSetId = Guid.NewGuid();

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
			string text = "test text";
			string duplicatedAnswer = "test answer 2";
			var answers = new Question.Answer[]
			{
				new Question.Answer(Guid.NewGuid(), "test answer 1"),
				new Question.Answer(Guid.NewGuid(), duplicatedAnswer),
				new Question.Answer(Guid.NewGuid(), duplicatedAnswer),
				new Question.Answer(Guid.NewGuid(), "test answer 4"),
			};
			string correctAnswer = answers[0].Text;
			Guid questionSetId = Guid.NewGuid();

			//act
			Action createQuestion = () => Question.Create(text, answers.ToHashSet(), correctAnswer, questionSetId);

			//assert
			createQuestion.Should().Throw<QuestionContainsDuplicatedAnswersException>()
				.WithMessage($"Question contains duplicated answers: {duplicatedAnswer}.");
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateQuestionWithEmptyCorrectAnswer_ThrowException(string correctAnswer)
		{
			//arrange
			string text = "test text";
			var answers = new Question.Answer[]
			{
				new Question.Answer(Guid.NewGuid(), "test answer 1"),
				new Question.Answer(Guid.NewGuid(), "test answer 2"),
				new Question.Answer(Guid.NewGuid(), "test answer 3"),
				new Question.Answer(Guid.NewGuid(), "test answer 4"),
			};
			Guid questionSetId = Guid.NewGuid();

			//act
			Action createQuestion = () => Question.Create(text, answers.ToHashSet(), correctAnswer, questionSetId);

			//assert
			createQuestion.Should().Throw<EmptyCorrectAnswerException>()
				.WithMessage("Correct answer can not be empty.");
		}

		[Fact]
		public void CreateQuestionWithAnswerThatIsNotOneOfAnswers_ThrowException()
		{
			//arrange
			string text = "test text";
			var answers = new Question.Answer[]
			{
				new Question.Answer(Guid.NewGuid(), "test answer 1"),
				new Question.Answer(Guid.NewGuid(), "test answer 2"),
				new Question.Answer(Guid.NewGuid(), "test answer 3"),
				new Question.Answer(Guid.NewGuid(), "test answer 4"),
			};
			string correctAnswer = Guid.NewGuid().ToString();
			Guid questionSetId = Guid.NewGuid();

			//act
			Action createQuestion = () => Question.Create(text, answers.ToHashSet(), correctAnswer, questionSetId);

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
				string text = "test text";

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
