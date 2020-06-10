namespace QuizApp.Application.Dtos
{
	public class QuestionSetsDto
	{
		public QuestionSetsElementDto[] Collection { get; set; }

		private QuestionSetsDto()
		{
		}

		public QuestionSetsDto(QuestionSetsElementDto[] collection)
		{
			Collection = collection;
		}
	}
}
