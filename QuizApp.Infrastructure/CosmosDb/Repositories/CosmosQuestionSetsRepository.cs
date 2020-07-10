using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Infrastructure.CosmosDb.Documents;
using QuizApp.Infrastructure.CosmosDb.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Repositories
{
	[CosmosDbRepository("QuestionSets", "/id")]
	public class CosmosQuestionSetsRepository : CosmosDbRepository<QuestionSetDocuments, Guid>, IQuestionSetsRepository
	{
		public CosmosQuestionSetsRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public async Task<IEnumerable<QuestionSet>> GetAllAsync() =>
			(await GetDocumentsAsync())?.ToModel();

		public async Task<QuestionSet> GetByIdAsync(Guid id) =>
			(await GetDocumentByIdAsync(id))?.ToModel();

		public async Task<QuestionSet> GetByNameAsync(string name) =>
			(await GetDocumentsAsync(x => x.Name == name)).FirstOrDefault()?.ToModel();

		public async Task<bool> ExistsAsync(Guid id) =>
			await CheckIfDocumentExists(id);

		public async Task AddAsync(QuestionSet questionSet) =>
			await AddDocumentAsync(questionSet.ToDocument());
	}
}
