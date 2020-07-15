using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class QuestionsService : IQuestionsService
	{
		private readonly IQuestionsRepository _questionsRepository;
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly ITagsRepository _tagsRepository;

		public QuestionsService(
			IQuestionsRepository questionsRepository,
			IQuestionSetsRepository questionSetsRepository,
			ITagsRepository tagsRepository)
		{
			_questionsRepository = questionsRepository;
			_questionSetsRepository = questionSetsRepository;
			_tagsRepository = tagsRepository;
		}

		public async Task<QuestionsDto> GetCollectionAsync()
		{
			var questions = await _questionsRepository.GetAllAsync();
			return questions.AsDto();
		}

		public async Task<QuestionDto> GetAsync(Guid id)
		{
			var question = await _questionsRepository.GetByIdAsync(id);

			if (question is null)
				throw new QuestionNotFoundException(id);

			return question.AsDto();
		}

		public async Task<Guid> CreateAsync(CreateQuestionDto createQuestionDto)
		{
			if (!await _questionSetsRepository.ExistsAsync(createQuestionDto.QuestionSetId))
				throw new QuestionSetNotFoundException(createQuestionDto.QuestionSetId);

			var tags = new HashSet<string>();
			foreach (var tag in createQuestionDto.Tags)
			{
				var existingTag = await _tagsRepository.GetByNameAsync(tag);
				if (existingTag is null)
					throw new TagNotFoundException(tag);
				tags.Add(tag);
			}

			var answers = createQuestionDto.Answers.Select(answer => Question.Answer.Create(answer.Text, answer.IsCorrect)).ToHashSet();
			var question = Question.Create(createQuestionDto.Text, answers, tags);
			await _questionsRepository.AddAsync(question);
			return question.Id;
		}

		public async Task RemoveAsync(Guid id)
		{
			if (!await _questionsRepository.ExistsAsync(id))
				throw new QuestionNotFoundException(id);

			await _questionsRepository.RemoveAsync(id);
		}
	}
}
