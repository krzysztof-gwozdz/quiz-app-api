using QuizApp.Core.Models;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IIdentitiesRepository : IRepository
	{
		Task AddAsync(Identity identity);
		Task<Identity> GetByUsernameAsync(string username);
		Task<bool> CheckIfExistsByUsernameAsync(string username);
	}
}