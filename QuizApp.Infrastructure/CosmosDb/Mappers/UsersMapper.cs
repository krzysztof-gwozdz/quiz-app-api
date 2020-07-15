using QuizApp.Core.Models;
using QuizApp.Infrastructure.CosmosDb.Documents;

namespace QuizApp.Infrastructure.CosmosDb.Mappers
{
	internal static class UsersMapper
	{
		public static UserDocument ToDocument(this User user) =>
			new UserDocument
			(
				user.Id,
				user.Username,
				user.PasswordHash,
				user.Salt
			);

		public static User ToModel(this UserDocument userDocument) =>
			new User
			(
				userDocument.Id,
				userDocument.Username,
				userDocument.PasswordHash,
				userDocument.Salt
			);
	}
}
