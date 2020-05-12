using System;
using System.Collections.Generic;

namespace QuizApp.Core.Models
{
	public class Quiz
	{
		public Guid Id { get; set; }
		public ISet<Question> Questions { get; set; }
	}
}
