using Microsoft.Azure.Documents;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb;
using QuizApp.Infrastructure.Entities;
using QuizApp.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class CosmosQuestionSetsRepository : CosmosDbRepository<QuestionSetEntity>, IQuestionSetsRepository
	{
		public CosmosQuestionSetsRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public override string CollectionName { get; } = "QuestionSets";
		public override Guid GenerateId(QuestionSetEntity entity) => Guid.NewGuid();
		public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId);

		public async Task<IEnumerable<QuestionSet>> GetAllAsync() =>
			(await GetDocumentsAsync()).Select(entity => entity.ToModel());

		public async Task<QuestionSet> GetByIdAsync(Guid id) =>
			(await GetDocumentByIdAsync(id)).ToModel();

		public async Task<QuestionSet> GetByNameAsync(string name) =>
			(await GetDocumentsAsync(x => x.Name == name)).FirstOrDefault()?.ToModel();

		public async Task<bool> ExistsAsync(Guid id) =>
			await CheckIfDocumentExists(id);

		public async Task AddAsync(QuestionSet questionSet) =>
			await AddDocumentAsync(questionSet.ToEntity());
	}
}
