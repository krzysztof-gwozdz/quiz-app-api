using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.Entities
{
	public class QuestionEntity : Entity
	{
		public string Text { get; set; }
		public ISet<AnswerEntity> Answers { get; set; }
		public Guid CorrectAnswerId { get; set; }
	}

	public class AnswerEntity : Entity
	{
		public string Text { get; set; }
	}
}
