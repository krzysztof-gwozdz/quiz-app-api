using QuizApp.Core.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuestionSetIconsRepository : IRepository
	{
		Task<QuestionSetIcon> GetAsync(Guid id);
		Task<bool> Exists(Guid id);
		Task AddAsync(QuestionSetIcon icon);
	}
}
