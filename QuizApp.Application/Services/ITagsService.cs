using QuizApp.Application.Dtos;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public interface ITagsService : IService
	{
		Task<TagsDto> GetCollectionAsync();
		Task<TagDto> GetAsync(string name);
		Task<string> CreateAsync(CreateTagDto dto);
	}
}