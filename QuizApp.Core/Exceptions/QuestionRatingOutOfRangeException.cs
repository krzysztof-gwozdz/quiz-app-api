using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class QuestionRatingIsOutOfRangeException : DomainException
	{
		public override string Code => "question_rating_is_out_of_range";

		public QuestionRatingIsOutOfRangeException(int? rating) : base($"Question rating: {rating} is out of range.")
		{
		}
	}
}
