using System;

namespace QuizApp.Core.Exceptions
{
	public class InvalidQuestionSetNameException : Exception
	{
		public InvalidQuestionSetNameException(string name) : base($"Question set name is invalid: {name}")
		{
		}
	}
}
