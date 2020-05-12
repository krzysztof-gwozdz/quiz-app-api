using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuestionSetsRepository
	{
		Task<IEnumerable<QuestionSet>> GetAllAsync();
		Task<QuestionSet> GetByIdAsync(Guid id);
	}
}
