using QuizApp.Core.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuestionsRepository
	{
		Task<Question> GetByIdAsync(Guid id);
		Task AddAsync(Question question);
		Task RemoveAsync(Guid id);
	}
}
