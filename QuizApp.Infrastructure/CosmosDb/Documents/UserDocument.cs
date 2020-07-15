using Newtonsoft.Json;
using System;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	internal class UserDocument
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("username")]
		public string Username { get; set; }

		[JsonProperty("passwordHash")]
		public string PasswordHash { get; set; }

		[JsonProperty("salt")]
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
