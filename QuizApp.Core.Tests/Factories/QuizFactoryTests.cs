﻿using FluentAssertions;
using Moq;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Factories;
using QuizApp.Core.Repositories;
using QuizApp.Core.Tests.Examples;
using QuizApp.Core.Tests.Mocks;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Core.Tests.Factories
{
	public class QuizFactoryTests
	{
		private readonly Mock<IQuestionsRepository> _questionsRepositoryMock;
		private readonly Mock<IQuestionSetsRepository> _questionSetsRepositoryMock;
		private readonly Mock<IRandomFactory> _randomFactory;
		private QuizFactory _quizFactory;

		public QuizFactoryTests()
		{
			_questionsRepositoryMock = new Mock<IQuestionsRepository>();
			_questionSetsRepositoryMock = new Mock<IQuestionSetsRepository>();
			_randomFactory = new Mock<IRandomFactory>();
			_quizFactory = new QuizFactory(_questionsRepositoryMock.Object, _questionSetsRepositoryMock.Object, _randomFactory.Object);
		}

		[Fact]
		public async Task GetQuizWithCorrectValues_QuizCreated()
		{
			//arrange
			var questionSetId = QuestionSetExample.NewId;
			int questionCount = 4;
			var questions = QuestionExample.GetValidQuestions(4, 4);
			_questionsRepositoryMock
				.Setup(x => x.GetAllBySetIdAsync(questionSetId))
				.ReturnsAsync(questions);
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(true);
			_questionsRepositoryMock
				.Setup(x => x.CountBySetIdAsync(questionSetId))
				.ReturnsAsync(questionCount + 1);
			_randomFactory
				.Setup(x => x.NextInt(questionCount))
				.Returns(0);

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
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(false);

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
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(true);

			//act
			Func<Task> getQuiz = async () => await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			await getQuiz.Should().ThrowAsync<NotEnoughQuestionsException>()
				.WithMessage($"Not enough question: {questionCount}. Min question count: {QuizFactory.MinQuestionCount}.");
		}

		[Fact]
		public async Task GetQuizWithMoreThanMaxNumberOfQuestionsForQuestionSet_ThrowException()
		{
			//arrange
			var questionSetId = QuestionSetExample.NewId;
			int questionCount = 11;
			int maxQuestionCount = 10;
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(true);
			_questionsRepositoryMock
				.Setup(x => x.CountBySetIdAsync(questionSetId))
				.ReturnsAsync(maxQuestionCount);

			//act
			Func<Task> getQuiz = async () => await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			await getQuiz.Should().ThrowAsync<TooManyQuestionsException>()
				.WithMessage($"Too many questions: {questionCount}. Max question count for this question set: {maxQuestionCount}.");
		}

		[Fact]
		public async Task GetQuizWithCorrectValues_QuestionsAreUnique()
		{
			var questionSetId = QuestionSetExample.NewId;
			var questionCount = 3;

			var questions = QuestionExample.GetValidQuestions(5, 4);
			_questionsRepositoryMock
				.Setup(x => x.GetAllBySetIdAsync(questionSetId))
				.ReturnsAsync(questions);
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(true);
			_questionsRepositoryMock
				.Setup(x => x.CountBySetIdAsync(questionSetId))
				.ReturnsAsync(questionCount + 1);

			var randomFactory = new MockRandomFactory(new[] { 0, 1, 1, 0, 2 });
			_quizFactory = new QuizFactory(_questionsRepositoryMock.Object, _questionSetsRepositoryMock.Object, randomFactory);

			//act
			var quiz = await _quizFactory.GetAsync(questionSetId, questionCount);

			//assert
			quiz.Id.Should().NotBeEmpty();
			quiz.Questions.Should().OnlyHaveUniqueItems(x => x.Id);
		}
	}
}
