using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	public static class UsersMapper
	{
		public static UserDocument ToDocument(this User model) =>
			new UserDocument(model.Id, model.Username, model.PasswordHash, model.Salt);

		public static User ToModel(this UserDocument document) =>
			new User(document.Id, document.Username, document.PasswordHash, document.Salt);
	}
}
