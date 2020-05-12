using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IQuizService
	{
		Task<QuizDto> Get(Guid id);
		Task<QuizSummaryDto> GetSummary(Guid id);
		Task<Guid> Generate(QuizParametersDto quizParameters);
		Task Solve(SolvedQuizDto solvedQuiz);
	}
}