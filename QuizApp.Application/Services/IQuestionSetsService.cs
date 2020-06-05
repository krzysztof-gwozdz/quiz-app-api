using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IQuestionSetsService : IService
	{
		Task<QuestionSetsDto> GetCollectionAsync();
		Task<QuestionSetDto> GetAsync(Guid id);
		Task<QuestionSetImageDto> GetImageAsync(Guid id);
		Task<Guid> CreateAsync(CreateQuestionSetDto dto);
	}
}