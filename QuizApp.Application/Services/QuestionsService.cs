using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class QuestionsService : IQuestionsService
	{
		private readonly IQuestionsRepository _questionsRepository;
		private readonly IQuestionSetsRepository _questionSetsRepository;

		public QuestionsService(
			IQuestionsRepository questionsRepository,
			IQuestionSetsRepository questionSetsRepository)
		{
			_questionsRepository = questionsRepository;
			_questionSetsRepository = questionSetsRepository;
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

			var answers = createQuestionDto.Answers.Select(answer => Question.Answer.Create(answer.Text, answer.IsCorrect)).ToHashSet();
			var question = Question.Create(createQuestionDto.Text, answers, createQuestionDto.QuestionSetId);
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
