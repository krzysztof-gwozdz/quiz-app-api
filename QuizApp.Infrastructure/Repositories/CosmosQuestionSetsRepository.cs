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

		public async Task<IEnumerable<QuestionSet>> GetAllAsync()
		{
			var entites = await GetDocumentsAsync();
			return entites.Select(entity => entity.FromEntity());
		}

		public async Task<QuestionSet> GetByIdAsync(Guid id)
		{
			var entity = await GetDocumentByIdAsync(id);
			return entity.FromEntity();
		}

		public async Task AddAsync(QuestionSet questionSet)
		{
			await AddDocumentAsync(questionSet.ToEntity());
		}
	}
}
