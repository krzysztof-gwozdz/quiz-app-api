using QuizApp.Application.Dtos;
using QuizApp.Application.Factories;
using QuizApp.Application.Mappers;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class QuizesService : IQuizesService
	{
		private IQuizesRepository _quizesRepository;
		private IQuestionsFactory _questionsFactory;

		public QuizesService(
			IQuizesRepository quizesRepository,
			IQuestionsFactory questionsFactory)
		{
			_quizesRepository = quizesRepository;
			_questionsFactory = questionsFactory;
		}

		public async Task<QuizDto> GetAsync(Guid id)
		{
			var entity = await _quizesRepository.GetByIdAsync(id);
			return entity.AsDto();
		}

		public async Task<QuizSummaryDto> GetSummaryAsync(Guid id)
		{
			// TODO
			throw new NotImplementedException();
		}

		public async Task<Guid> GenerateAsync(QuizParametersDto quizParameters)
		{
			var questions = await _questionsFactory.GetAsync(quizParameters.QuestionSetId, quizParameters.QuestionCount);
			var quiz = Quiz.Create(questions);
			await _quizesRepository.AddAsync(quiz);
			return quiz.Id;
		}
		
		public async Task SolveAsync(SolvedQuizDto solvedQuiz)
		{			
			// TODO
			throw new NotImplementedException();
		}
	}
}
