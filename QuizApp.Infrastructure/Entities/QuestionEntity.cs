using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.Entities
{
	public class QuestionEntity : Entity
	{
        [JsonProperty("text")]
		public string Text { get; set; }

        [JsonProperty("answers")]
		public ISet<AnswerEntity> Answers { get; set; }

        [JsonProperty("correctAnswerId")]
		public Guid CorrectAnswerId { get; set; }

		[JsonProperty("questionSetId")]
		public Guid QuestionSetId { get; set; }
	}

	public class AnswerEntity : Entity
	{
        [JsonProperty("text")]
		public string Text { get; set; }
	}
}
