using Newtonsoft.Json;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	internal class TagDocument
	{
		[JsonProperty("id")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		public TagDocument(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
