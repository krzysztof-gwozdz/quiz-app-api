using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class NotExactlyOneAnswerIsCorrectException : DomainException
	{
		public override string Code => "not exactly one answer is correct exception";

		public NotExactlyOneAnswerIsCorrectException(int correctAnswerCount) : base($"Not exactly one answer is correct exception. Correct answer count: {correctAnswerCount}")
		{
		}
	}
}
