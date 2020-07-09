namespace QuizApp.Application.Dtos
{
	public class TagsDto
	{
		public TagDtosElementDto[] Collection { get; set; }

		public TagsDto()
		{
		}

		public TagsDto(TagDtosElementDto[] collection)
		{
			Collection = collection;
		}
	}
}
