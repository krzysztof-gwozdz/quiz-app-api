using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class QuestionSetsService : IQuestionSetsService
	{
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly IQuestionsRepository _questionsRepository;

		public QuestionSetsService(
			IQuestionSetsRepository questionSetsRepository,
			IQuestionsRepository questionsRepository)
		{
			_questionSetsRepository = questionSetsRepository;
			_questionsRepository = questionsRepository;
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
				throw new QuestionSetDoesNotExistException(id);

			var totalQuestions = await _questionsRepository.CountBySetIdAsync(id);
			return questionSet.AsDto(totalQuestions);
		}

		public async Task<Guid> CreateAsync(CreateQuestionSetDto createQuestionSetDto)
		{
			var exitingQuestionSet = await _questionSetsRepository.GetByNameAsync(createQuestionSetDto.Name);
			if (exitingQuestionSet is { })
				throw new QuestionSetWithSelectedNameAlreadyExistsException(exitingQuestionSet.Name);

			var questionSet = QuestionSet.Create(createQuestionSetDto.Name, createQuestionSetDto.IconUrl, createQuestionSetDto.Color);
			await _questionSetsRepository.AddAsync(questionSet);

			return questionSet.Id;
		}
	}
}
