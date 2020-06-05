using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class QuestionSetImageIsToLargeException : DomainException
	{
		public override string Code => "question_set_image_is_to_large";

		public QuestionSetImageIsToLargeException(long imageSize, long maxImageSize)
			: base($"Question set image is to large: {imageSize}. Max image size: {maxImageSize}")
		{
		}
	}
}
