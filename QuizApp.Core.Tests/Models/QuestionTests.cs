using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class QuestionTests
	{

		[Fact]
		public void ConstructQuestions_QuestionCorrectAndAllAnswersCountAreCorrect()
		{
			//arrange
			var id = QuestionExample.NewId;
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = QuestionExample.ValidTags;
			var correctAnswersCount = 10;
			var allAnswersCount = 20;

			//act
			var question = new Question(id, text, answers, tags, correctAnswersCount, allAnswersCount);

			//assert
			question.CorrectAnswersCount.Should().Be(correctAnswersCount);
			question.AllAnswersCount.Should().Be(allAnswersCount);
		}

		[Theory]
		[InlineData(0, 0, 1)]
		[InlineData(1, 1, 1)]
		[InlineData(100, 100, 1)]
		[InlineData(1, 100, 0.01)]
		[InlineData(1, 2, 0.5)]
		[InlineData(1, 3, (double)1 / 3)]
		public void ConstructQuestions_QuestioRatioIsCorrect(int correctAnswersCount, int allAnswersCount, double ratioOfCorrectAnswers)
		{
			//arrange
			var id = QuestionExample.NewId;
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = QuestionExample.ValidTags;

			//act
			var question = new Question(id, text, answers, tags, correctAnswersCount, allAnswersCount);

			//assert
			question.RatioOfCorrectAnswers.Should().Be(ratioOfCorrectAnswers);
		}

		[Fact]
		public void CreateQuestionWithCorrectValues_QuestionCreated()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = QuestionExample.ValidTags;

			//act
			var question = Question.Create(text, answers, tags);

			//assert
			question.Id.Should().NotBeEmpty();
			question.Text.Should().Be(text);
			question.Answers.Should().BeEquivalentTo(answers);
			question.Tags.Should().BeEquivalentTo(tags);
			question.AllAnswersCount.Should().Be(0);
			question.CorrectAnswersCount.Should().Be(0);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void CreateQuestionWithEmptyText_ThrowException(string text)
		{
			//arrange
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = QuestionExample.ValidTags;

			//act
			Action createQuestion = () => Question.Create(text, answers, tags);

			//assert
			createQuestion.Should().Throw<EmptyQuestionTextException>()
				.WithMessage("Question text can not be empty.");
		}

		[Fact]
		public void CreateQuestionWithoutAnswers_ThrowException()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = (ISet<Question.Answer>)null;
			var tags = QuestionExample.ValidTags;

			//act
			Action createQuestion = () => Question.Create(text, answers, tags);

			//assert
			createQuestion.Should().Throw<InvalidNumberOfAnswersInQuestionException>()
				.WithMessage("Number of answers in question invalid: 0.");
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public void CreateQuestionWithLessThanMinNumberOfAnswers_ThrowException(int numberOfAnswers)
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = Enumerable.Range(0, numberOfAnswers).Select(x => QuestionExample.Answer.ValidInCorrectAnswer);
			var tags = QuestionExample.ValidTags;

			//act
			Action createQuestion = () => Question.Create(text, answers.ToHashSet(), tags);

			//assert
			createQuestion.Should().Throw<InvalidNumberOfAnswersInQuestionException>()
				.WithMessage($"Number of answers in question invalid: {numberOfAnswers}.");
		}

		[Fact]
		public void CreateQuestionWithDuplicatedAnswers_ThrowException()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var duplicatedAnswerText = Guid.NewGuid().ToString();
			var answers = new[]
			{
				QuestionExample.Answer.ValidCorrectAnswer,
				new Question.Answer(Guid.NewGuid(), duplicatedAnswerText, false),
				new Question.Answer(Guid.NewGuid(), duplicatedAnswerText, false),
				QuestionExample.Answer.ValidInCorrectAnswer,
			}.ToHashSet();
			var tags = QuestionExample.ValidTags;

			//act
			Action createQuestion = () => Question.Create(text, answers, tags);

			//assert
			createQuestion.Should().Throw<QuestionContainsDuplicatedAnswersException>()
				.WithMessage($"Question contains duplicated answers: {duplicatedAnswerText}.");
		}

		[Fact]
		public void CreateQuestionWithoutCorrectAnswers_ThrowException()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = new[]
			{
				QuestionExample.Answer.ValidInCorrectAnswer,
				QuestionExample.Answer.ValidInCorrectAnswer,
				QuestionExample.Answer.ValidInCorrectAnswer,
				QuestionExample.Answer.ValidInCorrectAnswer
			}.ToHashSet();
			var tags = QuestionExample.ValidTags;

			//act
			Action createQuestion = () => Question.Create(text, answers, tags);

			//assert
			createQuestion.Should().Throw<NotExactlyOneAnswerIsCorrectException>()
				.WithMessage("Not exactly one answer is correct exception. Correct answer count: 0");
		}

		[Fact]
		public void CreateQuestionWithMoreThanOneCorrectAnswer_ThrowException()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = new[]
			{
				QuestionExample.Answer.ValidInCorrectAnswer,
				QuestionExample.Answer.ValidInCorrectAnswer,
				QuestionExample.Answer.ValidCorrectAnswer,
				QuestionExample.Answer.ValidCorrectAnswer
			}.ToHashSet();
			var tags = QuestionExample.ValidTags;

			//act
			Action createQuestion = () => Question.Create(text, answers, tags);

			//assert
			createQuestion.Should().Throw<NotExactlyOneAnswerIsCorrectException>()
				.WithMessage("Not exactly one answer is correct exception. Correct answer count: 2");
		}

		[Fact]
		public void CreateQuestionWithTagsCollectionThatDoNotExist_ThrowException()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = (ISet<string>)null;

			//act
			Action createQuestion = () => Question.Create(text, answers, tags);

			//assert
			createQuestion.Should().Throw<EmptyQuestionTagsException>()
				.WithMessage($"Question tag collection can not be empty.");
		}

		[Fact]
		public void CreateQuestionWithEmptyTagCollection_ThrowException()
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = new HashSet<string>();

			//act
			Action createQuestion = () => Question.Create(text, answers, tags);

			//assert
			createQuestion.Should().Throw<EmptyQuestionTagsException>()
				.WithMessage($"Question tag collection can not be empty.");
		}

		public class AnswerTests
		{
			[Fact]
			public void CreateAnswerWithCorrectValues_AnswerCreated()
			{
				//arrange
				var text = QuestionExample.Answer.ValidText;
				var isCorrect = true;

				//act
				var answer = Question.Answer.Create(text, isCorrect);

				//assert
				answer.Id.Should().NotBeEmpty();
				answer.Text.Should().Be(text);
				answer.IsCorrect.Should().BeTrue();
			}

			[Theory]
			[InlineData(null)]
			[InlineData("")]
			[InlineData(" ")]
			public void CreateAnswerWithEmptyText_ThrowException(string text)
			{
				//arrange
				var isCorrect = true;

				//act
				Action createAnswer = () => Question.Answer.Create(text, isCorrect);

				//assert
				createAnswer.Should().Throw<EmptyAnswerTextException>()
					.WithMessage("Answer text can not be empty.");
			}
		}
	}
}
