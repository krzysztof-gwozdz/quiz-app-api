using QuizApp.Api.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Api.Services
{
	public interface IQuestionSetService
	{
		Task<QuestionSets> GetCollection();
		Task<QuestionSet> Get(Guid id);
	}
}