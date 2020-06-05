using Newtonsoft.Json;
using System;

namespace QuizApp.Infrastructure.Entities
{
	public class QuestionSetEntity : Entity
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("imageId")]
		public Guid ImageId { get; set; }

		[JsonProperty("color")]
		public string Color { get; set; }
	}
}
