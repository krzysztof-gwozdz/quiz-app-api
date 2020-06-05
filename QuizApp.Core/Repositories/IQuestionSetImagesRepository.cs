using QuizApp.Core.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuestionSetImagesRepository : IRepository
	{
		Task<QuestionSetImage> GetAsync(Guid id);
		Task<bool> Exists(Guid id);
		Task AddAsync(QuestionSetImage image);
	}
}
