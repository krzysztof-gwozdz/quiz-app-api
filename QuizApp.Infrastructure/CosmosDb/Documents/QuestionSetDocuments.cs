using Newtonsoft.Json;
using System;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class QuestionSetDocuments
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("tags")]
		public string[] Tags { get; set; }

		[JsonProperty("imageId")]
		public Guid ImageId { get; set; }

		[JsonProperty("color")]
		public string Color { get; set; }
	}
}
