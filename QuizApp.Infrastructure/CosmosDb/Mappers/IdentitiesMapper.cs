using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	public static class IdentitiesMapper
	{
		public static IdentityDocument ToDocument(this Identity model) =>
			new IdentityDocument(model.Id, model.Username, model.PasswordHash, model.Salt);
	}
}
