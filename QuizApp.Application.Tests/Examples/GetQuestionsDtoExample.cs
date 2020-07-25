using QuizApp.Application.Dtos;

namespace QuizApp.Application.Tests.Examples
{
	public static class GetQuestionsDtoExample
	{
		public static int ValidPageSize
			=> 3;

		public static int ValidPageNumber
			=> 2;

		public static GetQuestionsDto ValidDto =>
			new GetQuestionsDto(ValidPageSize, ValidPageNumber);
	}
}
