using System;

namespace QuizApp.Shared.Exceptions
{
	public class ValidationException : Exception
	{
		public ValidationError[] Errors { get; }

		public ValidationException(ValidationError[] errors) : base(string.Join<ValidationError>(Environment.NewLine, errors))
		{
			Errors = errors;
		}
	}

	public class ValidationError
	{
		public string Field { get; }
		public string Message { get; }

		public ValidationError(string field, string message)
		{
			Field = field;
			Message = message;
		}

		public override string ToString()
			=> $"{Field}: {Message}";
	}
}
