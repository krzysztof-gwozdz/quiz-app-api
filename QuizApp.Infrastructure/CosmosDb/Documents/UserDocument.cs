using System;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class UserDocument
	{
		public Guid Id { get; set; }
		public string Username { get; set; }
		public string PasswordHash { get; set; }
		public byte[] Salt { get; set; }

		public UserDocument(Guid id, string username, string passwordHash, byte[] salt)
		{
			Id = id;
			Username = username;
			PasswordHash = passwordHash;
			Salt = salt;
		}
	}
}
