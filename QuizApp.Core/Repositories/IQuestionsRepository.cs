using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuestionsRepository
	{
		Task<Question> GetByIdAsync(Guid id);
		Task<bool> ExistsAsync(Guid id);
		Task<ISet<Question>> GetAllBySetIdAsync(Guid setId);
		Task<int> CountBySetIdAsync(Guid setId);
		Task AddAsync(Question question);
		Task RemoveAsync(Guid id);
	}
}
