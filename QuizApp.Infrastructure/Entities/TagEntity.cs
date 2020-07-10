using Newtonsoft.Json;

namespace QuizApp.Infrastructure.Entities
{
	public class TagEntity
	{
		[JsonProperty("id")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
