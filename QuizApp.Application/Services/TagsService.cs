using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class TagsService : ITagsService
	{
		private readonly ITagsRepository _tagsRepository;

		public TagsService(ITagsRepository tagsRepository)
		{
			_tagsRepository = tagsRepository;
		}

		public async Task<TagsDto> GetCollectionAsync()
		{
			var tags = await _tagsRepository.GetAllAsync();
			return tags.AsDto();
		}

		public async Task<Guid> CreateAsync(CreateTagDto dto)
		{
			var existingTag = await _tagsRepository.GetByNameAsync(dto.Name);
			if (existingTag is { })
				throw new TagWithSelectedNameAlreadyExistsException(existingTag.Name);

			var questionSet = Tag.Create(dto.Name);
			await _tagsRepository.AddAsync(questionSet);

			return questionSet.Id;
		}
	}
}