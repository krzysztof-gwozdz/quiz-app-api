using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.IO;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class QuestionSetsService : IQuestionSetsService
	{
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly IQuestionsRepository _questionsRepository;
		private readonly IQuestionSetImagesRepository _questionSetImagesRepository;

		public QuestionSetsService(
			IQuestionSetsRepository questionSetsRepository,
			IQuestionsRepository questionsRepository,
			IQuestionSetImagesRepository questionSetImagesRepository)
		{
			_questionSetsRepository = questionSetsRepository;
			_questionsRepository = questionsRepository;
			_questionSetImagesRepository = questionSetImagesRepository;
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

			var totalQuestions = await _questionsRepository.CountBySetIdAsync(id);
			return questionSet.AsDto(totalQuestions);
		}

		public async Task<Stream> GetImageAsync(Guid id)
		{
			if (!await _questionSetImagesRepository.Exists(id))
				throw new QuestionSetImageNotFoundException(id);
			return (await _questionSetImagesRepository.GetAsync(id)).Data;
		}

		public async Task<Guid> CreateAsync(CreateQuestionSetDto dto)
		{
			var exitingQuestionSet = await _questionSetsRepository.GetByNameAsync(dto.Name);
			if (exitingQuestionSet is { })
				throw new QuestionSetWithSelectedNameAlreadyExistsException(exitingQuestionSet.Name);

			var image = QuestionSetImage.Create(dto.Image?.OpenReadStream(), dto.Image?.ContentType);
			var color = Color.Create(dto.Color);
			var questionSet = QuestionSet.Create(dto.Name, dto.Description, image.Id, color);
			await _questionSetsRepository.AddAsync(questionSet);
			await _questionSetImagesRepository.AddAsync(image);

			return questionSet.Id;
		}
	}
}
