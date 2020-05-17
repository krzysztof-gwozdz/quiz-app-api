using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuestionSetsRepository : IRepository
	{
		Task<IEnumerable<QuestionSet>> GetAllAsync();
		Task<QuestionSet> GetByIdAsync(Guid id);
		Task<QuestionSet> GetByNameAsync(string name);
		Task<bool> ExistsAsync(Guid id);
		Task AddAsync(QuestionSet questionSet);
	}
}
