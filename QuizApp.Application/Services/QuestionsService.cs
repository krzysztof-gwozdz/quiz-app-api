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
		private IQuestionsRepository _questionsRepository;
		private IQuestionSetsRepository _questionSetsRepository;

		public QuestionsService(
			IQuestionsRepository questionsRepository,
			IQuestionSetsRepository questionSetsRepository)
		{
			_questionsRepository = questionsRepository;
			_questionSetsRepository = questionSetsRepository;
		}

		public async Task<QuestionDto> GetAsync(Guid id)
		{
			var entity = await _questionsRepository.GetByIdAsync(id);
			return entity.AsDto();
		}

		public async Task<Guid> CreateAsync(CreateQuestionDto createQuestionDto)
		{
			if (!await _questionSetsRepository.ExistsAsync(createQuestionDto.QuestionSetId))
				throw new SelectedQuestionSetDoesNotExistException(createQuestionDto.QuestionSetId);

			var answers = createQuestionDto.Answers.Select(answer => Question.Answer.Create(answer.Text)).ToHashSet();
			var question = Question.Create(createQuestionDto.Text, answers, createQuestionDto.CorrectAnswer, createQuestionDto.QuestionSetId);
			await _questionsRepository.AddAsync(question);
			return question.Id;
		}

		public async Task RemoveAsync(Guid id)
		{
			await _questionsRepository.RemoveAsync(id);
		}
	}
}
