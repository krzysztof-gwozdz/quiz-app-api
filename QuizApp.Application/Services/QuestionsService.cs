using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
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

		public QuestionsService(IQuestionsRepository questionsRepository)
		{
			_questionsRepository = questionsRepository;
		}

		public async Task<QuestionDto> GetAsync(Guid id)
		{
			var entity = await _questionsRepository.GetByIdAsync(id);
			return entity.AsDto();
		}

		public async Task<Guid> CreateAsync(CreateQuestionDto createQuestionDto)
		{
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
