using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Infrastructure.CosmosDb.Documents;
using QuizApp.Infrastructure.CosmosDb.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Repositories
{
	[CosmosDbRepository("Users", "/id")]
	public class CosmosUsersRepository : CosmosDbRepository<UserDocument, string>, IUsersRepository
	{
		public CosmosUsersRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public async Task AddAsync(User user) =>
			await AddDocumentAsync(user.ToDocument());

		public async Task<User> GetByUsernameAsync(string username) =>
			(await GetDocumentsAsync(x => x.Username == username)).FirstOrDefault()?.ToModel();

		public async Task<bool> CheckIfExistsByUsernameAsync(string username) =>
			(await GetDocumentsAsync(x => x.Username == username)).Any();
	}
}
