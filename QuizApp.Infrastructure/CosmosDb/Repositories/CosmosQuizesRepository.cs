using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Infrastructure.Entities;
using QuizApp.Infrastructure.Mappers;
using System;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Repositories
{
	[CosmosDbRepository("Quizes", "/id")]
	public class CosmosQuizesRepository : CosmosDbRepository<QuizEntity>, IQuizesRepository
	{
		public CosmosQuizesRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public async Task<Quiz> GetByIdAsync(Guid id) =>
			(await GetDocumentByIdAsync(id)).ToModel();

		public async Task AddAsync(Quiz quiz) =>
			await AddDocumentAsync(quiz.ToEntity());

		public async Task Update(Quiz quiz) =>
			await UpdateDocumentAsync(quiz.ToEntity());
	}
}
