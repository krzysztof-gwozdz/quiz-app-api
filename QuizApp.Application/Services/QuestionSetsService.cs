using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class QuestionSetsService : IQuestionSetsService
	{
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly IQuestionsRepository _questionsRepository;
		private readonly IQuestionSetImagesRepository _questionSetImagesRepository;
		private readonly ITagsRepository _tagsRepository;

		public QuestionSetsService(
			IQuestionSetsRepository questionSetsRepository,
			IQuestionsRepository questionsRepository,
			IQuestionSetImagesRepository questionSetImagesRepository,
			ITagsRepository tagsRepository)
		{
			_questionSetsRepository = questionSetsRepository;
			_questionsRepository = questionsRepository;
			_questionSetImagesRepository = questionSetImagesRepository;
			_tagsRepository = tagsRepository;
		}

		public async Task<QuestionSetsDto> GetCollectionAsync()
		{
			var questionSets = await _questionSetsRepository.GetAllAsync();
			return questionSets.AsDto();
		}

		public async Task<QuestionSetDto> GetAsync(Guid id)
		{
			var questionSet = await _questionSetsRepository.GetByIdAsync(id);
			if (questionSet is null)
				throw new QuestionSetNotFoundException(id);

			var totalQuestions = await _questionsRepository.CountByTagsAsync(questionSet.Tags);
			return questionSet.AsDto(totalQuestions);
		}

		public async Task<QuestionSetImageDto> GetImageAsync(Guid id)
		{
			if (!await _questionSetImagesRepository.Exists(id))
				throw new QuestionSetImageNotFoundException(id);
			return (await _questionSetImagesRepository.GetAsync(id)).AsDto();
		}

		public async Task<Guid> CreateAsync(CreateQuestionSetDto dto)
		{
			var existingQuestionSet = await _questionSetsRepository.GetByNameAsync(dto.Name);
			if (existingQuestionSet is { })
				throw new QuestionSetWithSelectedNameAlreadyExistsException(existingQuestionSet.Name);

			var tags = new HashSet<string>();
			foreach (var tag in dto.Tags)
			{
				var existingTag = await _tagsRepository.GetByNameAsync(tag);
				if (existingTag is null)
					throw new TagNotFoundException(tag);
				tags.Add(tag);
			}

			var image = QuestionSetImage.Create(dto.Image?.OpenReadStream(), dto.Image?.ContentType);
			var color = Color.Create(dto.Color);
			var questionSet = QuestionSet.Create(dto.Name, tags, dto.Description, image.Id, color);
			await _questionSetsRepository.AddAsync(questionSet);
			await _questionSetImagesRepository.AddAsync(image);

			return questionSet.Id;
		}
	}
}
