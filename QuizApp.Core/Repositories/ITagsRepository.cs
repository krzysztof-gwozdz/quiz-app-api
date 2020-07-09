using QuizApp.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface ITagsRepository : IRepository
	{
		Task<IEnumerable<Tag>> GetAllAsync();
		Task<Tag> GetByIdAsync(Guid id);
		Task<Tag> GetByNameAsync(string name);
		Task AddAsync(Tag tag);
	}
}
