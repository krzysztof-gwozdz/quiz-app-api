using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IQuizzesService : IService
	{
		Task<QuizDto> GetAsync(Guid id);
		Task<QuizSummaryDto> GetSummaryAsync(Guid id);
		Task<Guid> GenerateAsync(QuizParametersDto quizParameters);
		Task SolveAsync(SolvedQuizDto solvedQuiz);
	}
}