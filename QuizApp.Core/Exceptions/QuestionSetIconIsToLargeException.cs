using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class QuestionSetIconIsToLargeException : DomainException
	{
		public override string Code => "question_set_icon_is_to_large";

		public QuestionSetIconIsToLargeException(long imageSize, long maxImageSize)
			: base($"Question set icon is to large: {imageSize}. Max image size: {maxImageSize}")
		{
		}
	}
}
