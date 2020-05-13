using Newtonsoft.Json;

namespace QuizApp.Infrastructure.Entities
{
	public class QuestionSetEntity : Entity
	{
        [JsonProperty("name")]
		public string Name { get; set; }

        [JsonProperty("iconUrl")]
		public string IconUrl { get; set; }

        [JsonProperty("color")]
		public string Color { get; set; }
	}
}
