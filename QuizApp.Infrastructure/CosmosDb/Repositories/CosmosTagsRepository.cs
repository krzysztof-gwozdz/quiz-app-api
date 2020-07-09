﻿using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Infrastructure.Entities;
using QuizApp.Infrastructure.Mappers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Repositories
{
	[CosmosDbRepository("Tags", "/id")]
	public class CosmosTagsRepository : CosmosDbRepository<TagEntity, string>, ITagsRepository
	{
		public CosmosTagsRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public async Task<IEnumerable<Tag>> GetAllAsync() =>
			(await GetDocumentsAsync())?.ToModel();

		public async Task<Tag> GetByNameAsync(string name) =>
			(await GetDocumentByIdAsync(name))?.ToModel();

		public async Task AddAsync(Tag questionSet) =>
			await AddDocumentAsync(questionSet.ToEntity());
	}
}
