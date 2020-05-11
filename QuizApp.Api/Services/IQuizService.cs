using QuizApp.Api.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Api.Services
{
	public interface IQuizService
	{
		Task<Quiz> Get(Guid id);
		Task<QuizSummary> GetSummary(Guid id);
		Task<Guid> Generate(QuizParameters quizParameters);
		Task Solve(SolvedQuiz solvedQuiz);
	}
}