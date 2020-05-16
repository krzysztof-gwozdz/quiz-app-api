﻿using Microsoft.Azure.Documents;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb;
using QuizApp.Infrastructure.Entities;
using QuizApp.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class CosmosQuestionsRepository : CosmosDbRepository<QuestionEntity>, IQuestionsRepository
	{
		public CosmosQuestionsRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public override string CollectionName { get; } = "Questions";
		public override Guid GenerateId(QuestionEntity entity) => Guid.NewGuid();
		public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId);

		public async Task<Question> GetByIdAsync(Guid id) =>
			(await GetDocumentByIdAsync(id)).ToModel();

		public async Task<bool> ExistsAsync(Guid id) =>
			await CheckIfDocumentExists(id);

		public async Task<ISet<Question>> GetAllBySetIdAsync(Guid setId) =>
			(await GetDocumentsAsync(x => x.QuestionSetId == setId)).ToModel();

		public async Task<int> CountBySetIdAsync(Guid setId) =>
			await CountDocumentsAsync(x => x.QuestionSetId == setId);

		public async Task AddAsync(Question question) =>
			await AddDocumentAsync(question.ToEntity());

		public async Task RemoveAsync(Guid id)
			=> await DeleteDocumentAsync(id);
	}
}
