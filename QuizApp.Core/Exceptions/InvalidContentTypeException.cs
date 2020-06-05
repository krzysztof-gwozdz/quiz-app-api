using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class InvalidContentTypeException : DomainException
	{
		public override string Code => "invalid_content_type";

		public InvalidContentTypeException(string contentType, params string[] validContentTypes)
			: base($"Invalid content type: {contentType}. Expected: {string.Join(", ", validContentTypes)}.")
		{
		}
	}
}
