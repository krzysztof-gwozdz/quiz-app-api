using System;
using System.Collections.Generic;

namespace QuizApp.Core.Models
{
	public class Question
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public ISet<Answer> Answers { get; set; }
		public Guid CorrectAnswerId { get; set; }
	}
}
