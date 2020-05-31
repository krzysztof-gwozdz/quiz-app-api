using QuizApp.Application.Dtos;
using System;
using System.IO;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface IQuestionSetsService : IService
	{
		Task<QuestionSetsDto> GetCollectionAsync();
		Task<QuestionSetDto> GetAsync(Guid id);
		Task<Stream> GetIconAsync(Guid id);
		Task<Guid> CreateAsync(CreateQuestionSetDto dto);
	}
}