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
			var id = QuestionExample.ValidId;
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = QuestionExample.ValidTags;
			var status = QuestionExample.ValidStatus;
			var correctAnswersCount = 10;
			var allAnswersCount = 20;

			//act
			var question = new Question(id, text, answers, tags, status, correctAnswersCount, allAnswersCount);

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
			var id = QuestionExample.ValidId;
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = QuestionExample.ValidTags;
			var status = QuestionExample.ValidStatus;

			//act
			var question = new Question(id, text, answers, tags, status,correctAnswersCount, allAnswersCount);

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
			question.Status.Should().BeEquivalentTo(Question.Statuses.New);
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
		public void CreateQuestionWithLessThanMinNumberOfAnswers_ThrowException(int answerCount)
		{
			//arrange
			var text = QuestionExample.ValidText;
			var answers = Enumerable.Range(0, answerCount).Select(x => QuestionExample.Answer.ValidInCorrectAnswer);
			var tags = QuestionExample.ValidTags;

			//act
			Action createQuestion = () => Question.Create(text, answers.ToHashSet(), tags);

			//assert
			createQuestion.Should().Throw<InvalidNumberOfAnswersInQuestionException>()
				.WithMessage($"Number of answers in question invalid: {answerCount}.");
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

		[Fact]
		public void EditQuestionWithCorrectValues_QuestionEdited()
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = QuestionExample.ValidTags;

			//act
			question.Edit(text, answers, tags);

			//assert
			question.Text.Should().Be(text);
			question.Answers.Should().BeEquivalentTo(answers);
			question.Tags.Should().BeEquivalentTo(tags);
		}

		[Fact]
		public void EditQuestionWhereOneIsNewOneShouldBeRemovedOneChangedIsCorrectOneChangedTextOneAnswerHasNotChanged_QuestionEdited()
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
			var oldAnswers = question.Answers.ToArray();
			var answers = new[]
			{
				QuestionExample.Answer.ValidCorrectAnswer, // New 
				// Removed
				new Question.Answer(oldAnswers[0].Id, oldAnswers[0].Text, false), // Changed IsCorrect
				new Question.Answer(oldAnswers[1].Id, QuestionExample.Answer.ValidText, oldAnswers[1].IsCorrect), // Changed Text
				oldAnswers[2] // Not changed
			};

			//act
			question.Edit(question.Text, answers.ToHashSet(), question.Tags);

			//assert
			question.Answers.Should().BeEquivalentTo(answers);
		}

		[Theory]
		[InlineData(null)]
		[InlineData("")]
		[InlineData(" ")]
		public void EditQuestionWithEmptyText_ThrowException(string text)
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = QuestionExample.ValidTags;

			//act
			Action editQuestion = () => question.Edit(text, answers, tags);

			//assert
			editQuestion.Should().Throw<EmptyQuestionTextException>()
				.WithMessage("Question text can not be empty.");
		}

		[Fact]
		public void EditQuestionWithoutAnswers_ThrowException()
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
			var text = QuestionExample.ValidText;
			var answers = (ISet<Question.Answer>)null;
			var tags = QuestionExample.ValidTags;

			//act
			Action editQuestion = () => question.Edit(text, answers, tags);

			//assert
			editQuestion.Should().Throw<InvalidNumberOfAnswersInQuestionException>()
				.WithMessage("Number of answers in question invalid: 0.");
		}

		[Theory]
		[InlineData(0)]
		[InlineData(1)]
		public void EditQuestionWithLessThanMinNumberOfAnswers_ThrowException(int answerCount)
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
			var text = QuestionExample.ValidText;
			var answers = Enumerable.Range(0, answerCount).Select(x => QuestionExample.Answer.ValidInCorrectAnswer);
			var tags = QuestionExample.ValidTags;

			//act
			Action editQuestion = () => question.Edit(text, answers.ToHashSet(), tags);

			//assert
			editQuestion.Should().Throw<InvalidNumberOfAnswersInQuestionException>()
				.WithMessage($"Number of answers in question invalid: {answerCount}.");
		}

		[Fact]
		public void EditQuestionWithDuplicatedAnswers_ThrowException()
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
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
			Action editQuestion = () => question.Edit(text, answers, tags);

			//assert
			editQuestion.Should().Throw<QuestionContainsDuplicatedAnswersException>()
				.WithMessage($"Question contains duplicated answers: {duplicatedAnswerText}.");
		}

		[Fact]
		public void EditQuestionWithoutCorrectAnswers_ThrowException()
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
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
			Action editQuestion = () => question.Edit(text, answers, tags);

			//assert
			editQuestion.Should().Throw<NotExactlyOneAnswerIsCorrectException>()
				.WithMessage("Not exactly one answer is correct exception. Correct answer count: 0");
		}

		[Fact]
		public void EditQuestionWithMoreThanOneCorrectAnswer_ThrowException()
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
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
			Action editQuestion = () => question.Edit(text, answers, tags);

			//assert
			editQuestion.Should().Throw<NotExactlyOneAnswerIsCorrectException>()
				.WithMessage("Not exactly one answer is correct exception. Correct answer count: 2");
		}

		[Fact]
		public void EditQuestionWithTagsCollectionThatDoNotExist_ThrowException()
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = (ISet<string>)null;

			//act
			Action editQuestion = () => question.Edit(text, answers, tags);

			//assert
			editQuestion.Should().Throw<EmptyQuestionTagsException>()
				.WithMessage($"Question tag collection can not be empty.");
		}

		[Fact]
		public void EditQuestionWithEmptyTagCollection_ThrowException()
		{
			//arrange
			var question = QuestionExample.GetValidQuestion(4);
			var text = QuestionExample.ValidText;
			var answers = QuestionExample.Answer.GetValidAnswers(4);
			var tags = new HashSet<string>();

			//act
			Action editQuestion = () => question.Edit(text, answers, tags);

			//assert
			editQuestion.Should().Throw<EmptyQuestionTagsException>()
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

			[Fact]
			public void EditAnswerWithCorrectValues_AnswerEdited()
			{
				//arrange
				var text = QuestionExample.Answer.ValidText;
				var isCorrect = false;
				var answer = QuestionExample.Answer.ValidCorrectAnswer;

				//act
				answer.Edit(text, isCorrect);

				//assert
				answer.Text.Should().Be(text);
				answer.IsCorrect.Should().BeFalse();
			}

			[Theory]
			[InlineData(null)]
			[InlineData("")]
			[InlineData(" ")]
			public void EditAnswerWithEmptyText_ThrowException(string text)
			{
				//arrange
				var answer = QuestionExample.Answer.ValidCorrectAnswer;
				var isCorrect = true;

				//act
				Action editAnswer = () => answer.Edit(text, isCorrect);

				//assert
				editAnswer.Should().Throw<EmptyAnswerTextException>()
					.WithMessage("Answer text can not be empty.");
			}
		}
	}
}
