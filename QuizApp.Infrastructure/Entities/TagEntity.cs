using Newtonsoft.Json;
using System;

namespace QuizApp.Infrastructure.Entities
{
	public class TagEntity
	{
		[JsonProperty("id")]
		public Guid Id { get; set; }

		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }
	}
}
