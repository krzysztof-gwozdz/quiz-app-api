using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface ITagsService : IService
	{
		Task<TagsDto> GetCollectionAsync();
		Task<TagDto> GetByIdAsync(Guid id);
		Task<TagDto> GetByNameAsync(string name);
		Task<Guid> CreateAsync(CreateTagDto dto);
	}
}