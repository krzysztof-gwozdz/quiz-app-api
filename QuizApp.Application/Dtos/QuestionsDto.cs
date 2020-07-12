namespace QuizApp.Application.Dtos
{
	public class QuestionsDto
	{
		public QuestionsElementDto[] Collection { get; set; }

		public QuestionsDto()
		{
		}

		public QuestionsDto(QuestionsElementDto[] collection)
		{
			Collection = collection;
		}
	}
}
