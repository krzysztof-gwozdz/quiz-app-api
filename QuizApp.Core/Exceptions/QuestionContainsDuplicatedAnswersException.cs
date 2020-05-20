using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class QuestionContainsDuplicatedAnswersException : DomainException
	{
		public override string Code => "question_contains_duplicated_answers";

		public QuestionContainsDuplicatedAnswersException(string duplicatedAnswer) : base($"Question contains duplicated answers: {duplicatedAnswer}.")
		{
		}
	}
}