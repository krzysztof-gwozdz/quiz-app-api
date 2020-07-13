using QuizApp.Core.Models;
using System.Threading.Tasks;

namespace QuizApp.Core.Repositories
{
	public interface IUsersRepository : IRepository
	{
		Task AddAsync(User user);
		Task<User> GetByUsernameAsync(string username);
		Task<bool> CheckIfExistsByUsernameAsync(string username);
	}
}