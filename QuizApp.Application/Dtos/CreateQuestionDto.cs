namespace QuizApp.Application.Dtos
{
	public class CreateQuestionDto
	{
		public string Text { get; set; }
		public CreateAnswerDto[] Answers { get; set; }
		public string CorrectAnswer { get; set; }
	}
}
