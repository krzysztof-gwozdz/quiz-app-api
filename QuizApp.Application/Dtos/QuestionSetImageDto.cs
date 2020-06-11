using System.IO;

namespace QuizApp.Application.Dtos
{
	public class QuestionSetImageDto
	{
		public Stream Data { get; set; }
		public string ContentType { get; set; }

		public QuestionSetImageDto()
		{
		}

		public QuestionSetImageDto(Stream data, string contentType)
		{
			Data = data;
			ContentType = contentType;
		}
	}
}
