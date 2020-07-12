using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.CosmosDb.Core;
using QuizApp.Infrastructure.CosmosDb.Documents;
using QuizApp.Infrastructure.CosmosDb.Mappers;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.CosmosDb.Repositories
{
	[CosmosDbRepository("Identities", "/id")]
	public class CosmosIdentityRepository : CosmosDbRepository<IdentityDocument, string>, IIdentitiesRepository
	{
		public CosmosIdentityRepository(ICosmosDbClientFactory factory) : base(factory)
		{
		}

		public async Task AddAsync(Identity identity) =>
			await AddDocumentAsync(identity.ToDocument());

		public async Task<bool> ExistsAsync(string username) =>
			(await GetDocumentsAsync(x => x.Username == username)).Any();
	}
}
