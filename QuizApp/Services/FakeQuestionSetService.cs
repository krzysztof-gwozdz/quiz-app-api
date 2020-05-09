﻿using QuizApp.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Services
{
	public class FakeQuestionSetService : IQuestionSetService
	{
		public Task<QuestionSet[]> GetCollection()
		{
			return Task.FromResult(new[]
			{
				new QuestionSet
				{
					Id = new Guid("5d4879a2-4cc1-4cb2-a1d3-1434dc2be63d"),
					Name = "C#",
					IconUrl = "",
					Color = "#A176DB",
				},
				new QuestionSet
				{
					Id = new Guid("9cd8593e-6cd7-43d7-9eff-dd139a85d5c6"),
					Name = "HTML",
					IconUrl = "",
					Color = "#E44D26",
				},
				new QuestionSet
				{
					Id = new Guid("bc6afea5-b7db-4a5f-83f7-2e1308532c68"),
					Name = "Javascript",
					IconUrl = "",
					Color = "#F7DF1E",
				}
			});
		}
	}
}
