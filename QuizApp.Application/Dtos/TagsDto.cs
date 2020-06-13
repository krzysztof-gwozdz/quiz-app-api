namespace QuizApp.Application.Dtos
{
	public class TagsDto
	{
		public TagDto[] Collection { get; set; }

		public TagsDto()
		{
		}

		public TagsDto(TagDto[] collection)
		{
			Collection = collection;
		}
	}
}
