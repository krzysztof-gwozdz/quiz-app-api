using Newtonsoft.Json;
using System;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	internal class TagDocument
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		public TagDocument(Guid id, string name, string description)
		{
			Id = id;
			Name = name;
			Description = description;
		}
	}
}
