namespace QuizApp.Application.Dtos
{
	public class QuestionSetsDto
	{
		public QuestionSetsElementDto[] Collection { get; set; }

		public QuestionSetsDto()
		{
		}

		public QuestionSetsDto(QuestionSetsElementDto[] collection)
		{
			Collection = collection;
		}
	}
}
