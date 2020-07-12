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

		[JsonProperty("tags")]
		public string[] Tags { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("imageId")]
		public Guid ImageId { get; set; }

		[JsonProperty("color")]
		public string Color { get; set; }

		public QuestionSetDocuments(Guid id, string name, string[] tags, string description, Guid imageId, string color)
		{
			Id = id;
			Name = name;
			Tags = tags;
			Description = description;
			ImageId = imageId;
			Color = color;
		}
	}
}
