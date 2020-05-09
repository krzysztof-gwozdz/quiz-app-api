using QuizApp.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Services
{
	public interface IQuestionSetService
	{
		Task<QuestionSets> GetCollection();
		Task<QuestionSet> Get(Guid id);
	}
}