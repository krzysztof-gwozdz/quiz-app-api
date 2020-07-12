using QuizApp.Core.Models;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IIdentitiesRepository : IRepository
	{
		Task AddAsync(Identity identity);
		Task<bool> ExistsAsync(string username);
	}
}