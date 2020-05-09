using QuizApp.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Services
{
	public interface IQuizService
	{
		Task<Quiz> Get(Guid id);
		Task<QuizSummary> GetSummary(Guid id);
		Task<Guid> Generate(QuizParameters quizParameters);
		Task Solve(SolvedQuiz solvedQuiz);
	}
}