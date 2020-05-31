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
		private readonly IQuestionSetIconsRepository _questionSetIconsRepository;

		public QuestionSetsService(
			IQuestionSetsRepository questionSetsRepository,
			IQuestionsRepository questionsRepository,
			IQuestionSetIconsRepository questionSetIconsRepository)
		{
			_questionSetsRepository = questionSetsRepository;
			_questionsRepository = questionsRepository;
			_questionSetIconsRepository = questionSetIconsRepository;
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

		public async Task<Stream> GetIconAsync(Guid id)
		{
			if (!await _questionSetIconsRepository.Exists(id))
				throw new QuestionSetIconNotFoundException(id);
			return (await _questionSetIconsRepository.GetAsync(id)).Data;
		}

		public async Task<Guid> CreateAsync(CreateQuestionSetDto dto)
		{
			var exitingQuestionSet = await _questionSetsRepository.GetByNameAsync(dto.Name);
			if (exitingQuestionSet is { })
				throw new QuestionSetWithSelectedNameAlreadyExistsException(exitingQuestionSet.Name);

			var icon = QuestionSetIcon.Create(dto.Icon?.OpenReadStream(), dto.Icon?.ContentType);
			var color = Color.Create(dto.Color);
			var questionSet = QuestionSet.Create(dto.Name, icon.Id, color);
			await _questionSetsRepository.AddAsync(questionSet);
			await _questionSetIconsRepository.AddAsync(icon);

			return questionSet.Id;
		}
	}
}
