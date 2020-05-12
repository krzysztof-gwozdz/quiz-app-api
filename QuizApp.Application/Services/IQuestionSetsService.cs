using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IQuestionSetsService
	{
		Task<QuestionSetDto> GetAsync(Guid id);
		Task<QuestionSetsDto> GetCollectionAsync();
	}
}