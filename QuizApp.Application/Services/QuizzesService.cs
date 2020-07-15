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
	public class QuizzesService : IQuizzesService
	{
		private readonly IQuizzesRepository _quizzesRepository;
		private readonly IQuizFactory _quizFactory;

		public QuizzesService(
			IQuizzesRepository quizzesRepository,
			IQuizFactory questionsFactory)
		{
			_quizzesRepository = quizzesRepository;
			_quizFactory = questionsFactory;
		}

		public async Task<QuizDto> GetAsync(Guid id)
		{
			var quiz = await _quizzesRepository.GetByIdAsync(id);
			if (quiz is null)
				throw new QuizNotFoundException(id);
			return quiz.AsQuizDto();
		}

		public async Task<QuizSummaryDto> GetSummaryAsync(Guid id)
		{
			var quiz = await _quizzesRepository.GetByIdAsync(id);
			if (quiz is null)
				throw new QuizNotFoundException(id);
			return quiz.AsQuizSummaryDto();
		}

		public async Task<Guid> GenerateAsync(QuizParametersDto quizParametersDto)
		{
			var quiz = await _quizFactory.GetAsync(quizParametersDto.QuestionSetId, quizParametersDto.QuestionCount);
			await _quizzesRepository.AddAsync(quiz);
			return quiz.Id;
		}

		public async Task SolveAsync(SolvedQuizDto solvedQuizDto)
		{
			var quiz = await _quizzesRepository.GetByIdAsync(solvedQuizDto.QuizId);
			if (quiz is null)
				throw new QuizNotFoundException(solvedQuizDto.QuizId);
			var playerAnswers = solvedQuizDto.PlayerAnswers.Select(playerAnswer => Quiz.PlayerAnswer.Create(playerAnswer.QuestionId, playerAnswer.AnswerId, playerAnswer.Rating)).ToHashSet();
			quiz.Solve(playerAnswers);
			await _quizzesRepository.Update(quiz);
		}
	}
}
