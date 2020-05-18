using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Factories;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class QuizesService : IQuizesService
	{
		private readonly IQuizesRepository _quizesRepository;
		private readonly IQuizFactory _quizFactory;

		public QuizesService(
			IQuizesRepository quizesRepository,
			IQuizFactory questionsFactory)
		{
			_quizesRepository = quizesRepository;
			_quizFactory = questionsFactory;
		}

		public async Task<QuizDto> GetAsync(Guid id)
		{
			var quiz = await _quizesRepository.GetByIdAsync(id);
			if (quiz is null)
				throw new QuizDoesNotExistException(id);
			return quiz.AsQuizDto();
		}

		public async Task<QuizSummaryDto> GetSummaryAsync(Guid id)
		{
			var quiz = await _quizesRepository.GetByIdAsync(id);
			if (quiz is null)
				throw new QuizDoesNotExistException(id);
			return quiz.AsQuizSummaryDto();
		}

		public async Task<Guid> GenerateAsync(QuizParametersDto quizParameters)
		{
			var quiz = await _quizFactory.GetAsync(quizParameters.QuestionSetId, quizParameters.QuestionCount);
			await _quizesRepository.AddAsync(quiz);
			return quiz.Id;
		}

		public async Task SolveAsync(SolvedQuizDto solvedQuiz)
		{
			var quiz = await _quizesRepository.GetByIdAsync(solvedQuiz.QuizId);
			if (quiz is null)
				throw new QuizDoesNotExistException(solvedQuiz.QuizId);
			var playerAnswers = solvedQuiz.PlayerAnswers.Select(playerAnswer => Quiz.PlayerAnswer.Create(playerAnswer.QuestionId, playerAnswer.AnswerId)).ToHashSet();
			quiz.Solve(playerAnswers);
			await _quizesRepository.Update(quiz);
		}
	}
}
