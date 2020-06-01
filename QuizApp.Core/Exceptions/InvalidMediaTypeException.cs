using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class InvalidMediaTypeException : DomainException
	{
		public override string Code => "invalid_media_type";

		public InvalidMediaTypeException(string mediaType, params string[] validMediaTypes)
			: base($"Invalid media type: {mediaType}. Expected: {string.Join(", ", validMediaTypes)}.")
		{
		}
	}
}
