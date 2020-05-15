using System;

namespace QuizApp.Core.Exceptions
{
	public class EmptyAnswerTextException : Exception
	{
		public EmptyAnswerTextException() : base("Answer text can not be empty.")
		{
		}
	}
}
