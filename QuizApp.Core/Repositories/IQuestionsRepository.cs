﻿using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IQuestionsRepository : IRepository
	{
		Task<IEnumerable<Question>> GetAllAsync();
		Task<IEnumerable<Question>> GetAsync(int pageSize, int pageNumber);
		Task<Question> GetByIdAsync(Guid id);
		Task<bool> ExistsAsync(Guid id);
		Task<ISet<Question>> GetAllByTagsAsync(ISet<string> tags);
		Task<int> CountByTagAsync(string tag);
		Task<int> CountByTagsAsync(ISet<string> tags);
		Task AddAsync(Question question);
		Task UpdateAsync(Question question);
		Task RemoveAsync(Guid id);
	}
}
