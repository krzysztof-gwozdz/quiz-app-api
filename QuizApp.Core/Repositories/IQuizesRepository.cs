using QuizApp.Core.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuizesRepository
	{
		Task<Quiz> GetByIdAsync(Guid id);
		Task AddAsync(Quiz quiz);
		Task Update(Quiz quiz);
	}
}
