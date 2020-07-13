using Newtonsoft.Json;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class TagDocument
	{
		[JsonProperty("id")]
		public string Name { get; set; }
		public string Description { get; set; }

		public TagDocument(string name, string description)
		{
			Name = name;
			Description = description;
		}
	}
}
