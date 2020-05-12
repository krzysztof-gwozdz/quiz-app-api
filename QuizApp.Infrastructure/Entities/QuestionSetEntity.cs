using QuizApp.Core.Models;
using System;

namespace QuizApp.Infrastructure.Entities
{
	public class QuestionSetEntity : Entity
	{
		public string Name { get; set; }
		public string IconUrl { get; set; }
		public string Color { get; set; }
	}
}
