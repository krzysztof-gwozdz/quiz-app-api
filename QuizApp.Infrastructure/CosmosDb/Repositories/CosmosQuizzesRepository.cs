using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Infrastructure.CosmosDb.Documents;
using QuizApp.Infrastructure.CosmosDb.Mappers;
using System;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Repositories
{
	[CosmosDbRepository("Quizzes", "/id")]
	public class CosmosQuizzesRepository : CosmosDbRepository<QuizDocument, Guid>, IQuizzesRepository
	{
		public CosmosQuizzesRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public async Task<Quiz> GetByIdAsync(Guid id) =>
			(await GetDocumentByIdAsync(id))?.ToModel();

		public async Task AddAsync(Quiz quiz) =>
			await AddDocumentAsync(quiz.ToDocument());

		public async Task Update(Quiz quiz) =>
			await UpdateDocumentAsync(quiz.Id, quiz.ToDocument());
	}
}
