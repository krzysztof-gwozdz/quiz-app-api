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
		private readonly ITagsRepository _tagsRepository;

		public QuestionsService(
			IQuestionsRepository questionsRepository,
			ITagsRepository tagsRepository)
		{
			_questionsRepository = questionsRepository;
			_tagsRepository = tagsRepository;
		}

		public async Task<QuestionsDto> GetCollectionAsync(GetQuestionsDto getQuestionsDto)
		{
			int pageSize = getQuestionsDto.PageSize ?? 10;
			int pageNumber = getQuestionsDto.PageNumber ?? 1;
			var questions = await _questionsRepository.GetAsync(pageSize, pageNumber);
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

		public async Task EditAsync(EditQuestionDto editQuestionDto)
		{
			var question = await _questionsRepository.GetByIdAsync(editQuestionDto.Id);
			if (question is null)
				throw new QuestionNotFoundException(editQuestionDto.Id);

			var tags = new HashSet<string>();
			foreach (var tag in editQuestionDto.Tags)
			{
				var existingTag = await _tagsRepository.GetByNameAsync(tag);
				if (existingTag is null)
					throw new TagNotFoundException(tag);
				tags.Add(tag);
			}

			var answers = editQuestionDto.Answers.Select(answer => Question.Answer.Create(answer.Text, answer.IsCorrect)).ToHashSet();
			question.Edit(editQuestionDto.Text, answers, tags);
			await _questionsRepository.UpdateAsync(question);
		}

		public async Task RemoveAsync(Guid id)
		{
			if (!await _questionsRepository.ExistsAsync(id))
				throw new QuestionNotFoundException(id);

			await _questionsRepository.RemoveAsync(id);
		}
	}
}
