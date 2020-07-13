using System;

namespace QuizApp.Infrastructure.CosmosDb.Documents
{
	public class QuestionSetDocuments
	{
		public Guid Id { get; set; }
		public string Name { get; set; }
		public string[] Tags { get; set; }
		public string Description { get; set; }
		public Guid ImageId { get; set; }
		public string Color { get; set; }

		public QuestionSetDocuments(Guid id, string name, string[] tags, string description, Guid imageId, string color)
		{
			Id = id;
			Name = name;
			Tags = tags;
			Description = description;
			ImageId = imageId;
			Color = color;
		}
	}
}
