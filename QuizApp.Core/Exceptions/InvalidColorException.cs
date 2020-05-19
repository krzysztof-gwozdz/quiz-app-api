using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class InvalidColorException : DomainException
	{
		public override string Code => "invalid_color";

		public InvalidColorException(string value) : base($"Color: {value} is invalid.")
		{
		}
	}
}
