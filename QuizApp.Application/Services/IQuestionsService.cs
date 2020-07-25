using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IQuestionsService : IService
	{
		Task<QuestionsDto> GetCollectionAsync(GetQuestionsDto getQuestionsDto);
		Task<QuestionDto> GetAsync(Guid id);
		Task<Guid> CreateAsync(CreateQuestionDto createQuestionDto);
		Task EditAsync(EditQuestionDto editQuestionDto);
		Task RemoveAsync(Guid id);
	}
}