using QuizApp.Application.Dtos;
using QuizApp.Application.Mappers;
using QuizApp.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class QuestionSetsService : IQuestionSetsService
	{
		private IQuestionSetsRepository _questionSetsRepository;

		public QuestionSetsService(IQuestionSetsRepository questionSetsRepository)
		{
			_questionSetsRepository = questionSetsRepository;
		}

		public async Task<QuestionSetsDto> GetCollectionAsync()
		{
			var entities = await _questionSetsRepository.GetAllAsync();
			return entities.AsDto();
		}

		public async Task<QuestionSetDto> GetAsync(Guid id)
		{
			var entity = await _questionSetsRepository.GetByIdAsync(id);
			return entity.AsDto();	
		}		
	}
}
