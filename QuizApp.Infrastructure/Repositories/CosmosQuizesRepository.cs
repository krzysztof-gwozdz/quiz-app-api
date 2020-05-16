using Microsoft.Azure.Documents;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb;
using QuizApp.Infrastructure.Entities;
using QuizApp.Infrastructure.Mappers;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class CosmosQuizesRepository : CosmosDbRepository<QuizEntity>, IQuizesRepository
	{
		public CosmosQuizesRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public override string CollectionName { get; } = "Quizes";
		public override Guid GenerateId(QuizEntity entity) => Guid.NewGuid();
		public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId);

		public async Task<Quiz> GetByIdAsync(Guid id) =>
			(await GetDocumentByIdAsync(id)).ToModel();

		public async Task AddAsync(Quiz quiz) =>
			await AddDocumentAsync(quiz.ToEntity());

		public async Task Update(Quiz quiz) =>
			await UpdateDocumentAsync(quiz.ToEntity());
	}
}
