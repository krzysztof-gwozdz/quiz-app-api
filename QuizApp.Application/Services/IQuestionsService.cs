using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IQuestionsService
	{
		Task<QuestionDto> GetAsync(Guid id);
		Task<Guid> CreateAsync(CreateQuestionDto createQuestionDto);
		Task RemoveAsync(Guid id);
	}
}