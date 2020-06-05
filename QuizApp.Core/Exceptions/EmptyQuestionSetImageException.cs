using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionSetImageException : DomainException
	{
		public override string Code => "empty_question_set_image";

		public EmptyQuestionSetImageException() : base("Question set image can not be empty.")
		{
		}
	}
}
