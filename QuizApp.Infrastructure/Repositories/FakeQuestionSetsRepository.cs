﻿using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.Repositories
{
	public class FakeQuestionSetsRepository : IQuestionSetsRepository
	{
		public async Task<IEnumerable<QuestionSet>> GetAllAsync() => 
			new[]
			{
				new QuestionSet(new Guid("5d4879a2-4cc1-4cb2-a1d3-1434dc2be63d"), "C#", "", "#A176DB"),
				new QuestionSet(new Guid("9cd8593e-6cd7-43d7-9eff-dd139a85d5c6"), "HTML", "", "#E44D26"),
				new QuestionSet(new Guid("bc6afea5-b7db-4a5f-83f7-2e1308532c68"), "Javascript", "", "#F7DF1E")
			};

		public async Task<QuestionSet> GetByIdAsync(Guid id) =>
			new QuestionSet(id, "Javascript", "", "#F7DF1E");
	}
}
