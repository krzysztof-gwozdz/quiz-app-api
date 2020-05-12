using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IQuestionSetService
	{
		Task<QuestionSetsDto> GetCollection();
		Task<QuestionSetDto> Get(Guid id);
	}
}