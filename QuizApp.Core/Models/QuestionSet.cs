using System;

namespace QuizApp.Core.Models
{
	public class QuestionSet
	{
		public Guid Id { get; }
		public string Name { get; }
		public string IconUrl { get; }
		public string Color { get; }

		public QuestionSet(Guid id, string name, string iconUrl, string color)
		{
			Id = id;
			Name = name;
			IconUrl = iconUrl;
			Color = color;			
		}
	}
}
