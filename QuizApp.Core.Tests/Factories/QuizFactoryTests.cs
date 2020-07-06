using FluentAssertions;
using NSubstitute;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Factories;
using QuizApp.Core.Repositories;
using QuizApp.Core.Tests.Examples;
using QuizApp.Core.Tests.Mocks;
using QuizApp.Shared.Exceptions;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Core.Tests.Factories
{
	public class QuizFactoryTests
	{
		private readonly IQuestionsRepository _questionsRepository;
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly IRandomFactory _randomFactory;
		private QuizFactory _quizFactory;

		public QuizFactoryTests()
		{
			_questionsRepository = Substitute.For<IQuestionsRepository>();
			_questionSetsRepository = Substitute.For<IQuestionSetsRepository>();
			_randomFactory = Substitute.For<IRandomFactory>();
			_quizFactory = new QuizFactory(_questionsRepository, _questionSetsRepository, _randomFactory);
		}

		[Fact]
		public async Task GetQuizWithCorrectValues_QuizCreated()
		{
			//arrange
			var questionSetId = QuestionSetExample.NewId;
			int questionCount = 4;
			var questions = QuestionExample.GetValidQuestions(4, 4);

			_questionsRepository.GetAllBySetIdAsync(questionSetId).Returns(questions);
			_questionSetsRepository.ExistsAsync(questionSetId).Returns(true);
			_questionsRepository.CountBySetIdAsync(questionSetId).Returns(questionCount + 1);
			_randomFactory.NextInt(questionCount).Returns(0);

			//act
			var quiz = await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			quiz.Id.Should().NotBeEmpty();
			quiz.Questions.Should().HaveCount(questionCount);
		}

		[Fact]
		public async Task GetQuizWithQuestionSetThatDoesNotExists_ThrowException()
		{
			//arrange
			var questionSetId = QuestionSetExample.NewId;
			_questionSetsRepository.ExistsAsync(questionSetId).Returns(false);

			//act
			Func<Task> getQuiz = async () => await _quizFactory.GetAsync(questionSetId, 2);

			//assert
			await getQuiz.Should().ThrowAsync<QuestionSetNotFoundException>()
				.WithMessage($"Question set: {questionSetId} not found.");
		}

		[Fact]
		public async Task GetQuizWithLessThanMinimumQuestions_ThrowException()
		{
			//arrange
			var questionSetId = QuestionSetExample.NewId;
			int questionCount = 1;
			_questionSetsRepository.ExistsAsync(questionSetId).Returns(true);

			//act
			Func<Task> getQuiz = async () => await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			await getQuiz.Should().ThrowAsync<ValidationException>()
				.WithMessage(
					$"questionCount: Not enough question: {questionCount}. Min question count: {QuizFactory.MinQuestionCount}.{Environment.NewLine}" +
					$"questionCount: Too many questions: {questionCount}. Max question count for this question set: 0."
				);
		}

		[Fact]
		public async Task GetQuizWithMoreThanMaxNumberOfQuestionsForQuestionSet_ThrowException()
		{
			//arrange
			var questionSetId = QuestionSetExample.NewId;
			int questionCount = 11;
			int maxQuestionCount = 10;
			_questionSetsRepository.ExistsAsync(questionSetId).Returns(true);
			_questionsRepository.CountBySetIdAsync(questionSetId).Returns(maxQuestionCount);

			//act
			Func<Task> getQuiz = async () => await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			await getQuiz.Should().ThrowAsync<ValidationException>()
				.WithMessage($"questionCount: Too many questions: {questionCount}. Max question count for this question set: {maxQuestionCount}.");
		}

		[Fact]
		public async Task GetQuizWithCorrectValues_QuestionsAreUnique()
		{
			var questionSetId = QuestionSetExample.NewId;
			var questionCount = 3;

			var questions = QuestionExample.GetValidQuestions(5, 4);
			_questionsRepository.GetAllBySetIdAsync(questionSetId).Returns(questions);
			_questionSetsRepository.ExistsAsync(questionSetId).Returns(true);
			_questionsRepository.CountBySetIdAsync(questionSetId).Returns(questionCount + 1);

			var randomFactory = new RandomFactoryMock(new[] { 0, 1, 1, 0, 2 });
			_quizFactory = new QuizFactory(_questionsRepository, _questionSetsRepository, randomFactory);

			//act
			var quiz = await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			quiz.Id.Should().NotBeEmpty();
			quiz.Questions.Should().OnlyHaveUniqueItems(x => x.Id);
		}
	}
}
