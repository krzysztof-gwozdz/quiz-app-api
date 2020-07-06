using Newtonsoft.Json;

namespace QuizApp.Infrastructure.Entities
{
	public class TagEntity : Entity
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
