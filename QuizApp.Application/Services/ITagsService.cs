using QuizApp.Application.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface ITagsService : IService
	{
		Task<TagsDto> GetCollectionAsync();
		Task<Guid> CreateAsync(CreateTagDto dto);
	}
}