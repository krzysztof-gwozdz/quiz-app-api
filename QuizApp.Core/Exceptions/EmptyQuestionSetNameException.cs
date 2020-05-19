using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionSetNameException : DomainException
	{
		public override string Code => "empty_question_set_name";

		public EmptyQuestionSetNameException() : base("Question set name can not be empty.")
		{
		}
	}
}
