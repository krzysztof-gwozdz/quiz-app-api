using FluentAssertions;
using Moq;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Servives
{
	public class QuestionsServiceTests
	{
		private readonly Mock<IQuestionSetsRepository> _questionSetsRepositoryMock;
		private readonly Mock<IQuestionsRepository> _questionsRepositoryMock;
		private readonly QuestionsService _questionsService;

		public QuestionsServiceTests()
		{
			_questionSetsRepositoryMock = new Mock<IQuestionSetsRepository>();
			_questionsRepositoryMock = new Mock<IQuestionsRepository>();
			_questionsService = new QuestionsService(_questionsRepositoryMock.Object, _questionSetsRepositoryMock.Object);
		}

		[Fact]
		public async Task CreateQuestionCorrectValues()
		{
			//arrange
			var questionSetId = Guid.NewGuid();
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(true);

			string text = "test text";
			var answers = new CreateAnswerDto[]
			{
				new CreateAnswerDto{ Text = "test answer 1" },
				new CreateAnswerDto{ Text = "test answer 2" },
				new CreateAnswerDto{ Text = "test answer 3" },
				new CreateAnswerDto{ Text = "test answer 4" },
			};
			string correctAnswer = answers[0].Text;
			var dto = new CreateQuestionDto { Text = text, Answers = answers, CorrectAnswer = correctAnswer, QuestionSetId = questionSetId };

			//act 
			var questionId = await _questionsService.CreateAsync(dto);

			//assert
			questionId.Should().NotBeEmpty();
		}

		[Fact]
		public async Task CreateQuestionWithQuestionSetIdThatDoesNotExist_ThrowException()
		{
			//arrange
			var questionSetId = Guid.NewGuid();
			_questionSetsRepositoryMock
				.Setup(x => x.ExistsAsync(questionSetId))
				.ReturnsAsync(false);

			string text = "test text";
			var answers = new CreateAnswerDto[]
			{
				new CreateAnswerDto{ Text = "test answer 1" },
				new CreateAnswerDto{ Text = "test answer 2" },
				new CreateAnswerDto{ Text = "test answer 3" },
				new CreateAnswerDto{ Text = "test answer 4" },
			};
			string correctAnswer = answers[0].Text;
			var dto = new CreateQuestionDto { Text = text, Answers = answers, CorrectAnswer = correctAnswer, QuestionSetId = questionSetId };

			//act 
			Func<Task> createQuestion = async () => await _questionsService.CreateAsync(dto);

			//assert
			await createQuestion.Should().ThrowAsync<SelectedQuestionSetDoesNotExistException>()
				.WithMessage($"Selected question set: {questionSetId} does not exist.");
		}
	}
}
