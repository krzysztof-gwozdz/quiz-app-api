namespace QuizApp.Application.Dtos
{
	public class GetQuestionsDto
	{
		public int? PageSize { get; set; }
		public int? PageNumber { get; set; }

		public GetQuestionsDto()
		{
		}

		public GetQuestionsDto(int pageSize, int pageNumber)
		{
			PageSize = pageSize;
			PageNumber = pageNumber;
		}
	}
}