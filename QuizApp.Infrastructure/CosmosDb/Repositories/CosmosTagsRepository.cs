﻿using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Infrastructure.CosmosDb.Documents;
using QuizApp.Infrastructure.CosmosDb.Mappers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Repositories
{
	[CosmosDbRepository("Tags", "/id")]
	internal class CosmosTagsRepository : CosmosDbRepository<TagDocument, string>, ITagsRepository
	{
		public CosmosTagsRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public async Task<IEnumerable<Tag>> GetAllAsync() =>
			(await GetDocumentsAsync())?.ToModel();

		public async Task<Tag> GetByNameAsync(string name) =>
			(await GetDocumentsAsync(x => x.Name == name))?.FirstOrDefault()?.ToModel();

		public async Task AddAsync(Tag tag) =>
			await AddDocumentAsync(tag.ToDocument());
	}
}
