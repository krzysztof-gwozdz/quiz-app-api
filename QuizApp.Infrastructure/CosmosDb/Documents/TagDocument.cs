using Newtonsoft.Json;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class TagDocument
	{
		[JsonProperty("id")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
