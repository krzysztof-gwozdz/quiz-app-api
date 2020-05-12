using QuizApp.Core.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuizesRepository
	{
		Task<Quiz> GetAsync(Guid id);
	}
}
