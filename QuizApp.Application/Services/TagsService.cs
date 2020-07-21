using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class TagsService : ITagsService
	{
		private readonly ITagsRepository _tagsRepository;
		private readonly IQuestionsRepository _questionsRepository;

		public TagsService(
			ITagsRepository tagsRepository,
			IQuestionsRepository questionsRepository)
		{
			_tagsRepository = tagsRepository;
			_questionsRepository = questionsRepository;
		}

		public async Task<TagsDto> GetCollectionAsync()
		{
			var tags = await _tagsRepository.GetAllAsync();
			return tags.AsDto();
		}

		public async Task<TagDto> GetAsync(string name)
		{
			var tag = await _tagsRepository.GetByNameAsync(name);
			if (tag is null)
				throw new TagNotFoundException(name);

			var totalQuestions = await _questionsRepository.CountByTagAsync(name);
			return tag.AsDto(totalQuestions);
		}

		public async Task<string> CreateAsync(CreateTagDto dto)
		{
			var existingTag = await _tagsRepository.GetByNameAsync(dto.Name);
			if (existingTag is { })
				throw new TagWithSelectedNameAlreadyExistsException(existingTag.Name);

			var tag = Tag.Create(dto.Name, dto.Description);
			await _tagsRepository.AddAsync(tag);

			return tag.Name;
		}
	}
}