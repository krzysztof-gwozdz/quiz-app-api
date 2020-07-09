using QuizApp.Core.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface ITagsRepository : IRepository
	{
		Task<IEnumerable<Tag>> GetAllAsync();
		Task<Tag> GetByNameAsync(string name);
		Task AddAsync(Tag tag);
	}
}
