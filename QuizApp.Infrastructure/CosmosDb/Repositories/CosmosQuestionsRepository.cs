﻿using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Infrastructure.Entities;
using QuizApp.Infrastructure.Mappers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Repositories
{
	[CosmosDbRepository("Questions", "/id")]
	public class CosmosQuestionsRepository : CosmosDbRepository<QuestionEntity, Guid>, IQuestionsRepository
	{
		public CosmosQuestionsRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public async Task<Question> GetByIdAsync(Guid id) =>
			(await GetDocumentByIdAsync(id))?.ToModel();

		public async Task<bool> ExistsAsync(Guid id) =>
			await CheckIfDocumentExists(id);

		public async Task<ISet<Question>> GetAllByTagsAsync(ISet<string> tags) =>
			(await GetDocumentsAsync(question => question.Tags.Any(tag => tags.Contains(tag))))?.ToModel();

		public async Task<int> CountByTagsAsync(ISet<string> tags) =>
			await CountDocumentsAsync(question => question.Tags.Any(tag => tags.Contains(tag)));

		public async Task AddAsync(Question question) =>
			await AddDocumentAsync(question.ToEntity());

		public async Task RemoveAsync(Guid id)
			=> await DeleteDocumentAsync(id);
	}
}
