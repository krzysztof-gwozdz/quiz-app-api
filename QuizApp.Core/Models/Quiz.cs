using System;
using System.Collections.Generic;

namespace QuizApp.Core.Models
{
	public class Quiz
	{
		public Guid Id { get; set; }
		public Question[] Questions { get; set; }

		public Quiz(Guid id, Question[] questions)
		{
			Id = id;
			Questions = questions;
		}

		public static Quiz Create(Question[] questions)
		{
			return new Quiz(Guid.NewGuid(), questions);
		}
	}
}
