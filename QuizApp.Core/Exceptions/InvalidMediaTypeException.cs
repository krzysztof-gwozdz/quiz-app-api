using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class InvalidMediaTypeException : DomainException
	{
		public override string Code => "invalid_media_type";

		public InvalidMediaTypeException(string mediaType, string expectedMediaType)
			: base($"Invalid media type: {mediaType}. Expected: {expectedMediaType}")
		{
		}
	}
}
