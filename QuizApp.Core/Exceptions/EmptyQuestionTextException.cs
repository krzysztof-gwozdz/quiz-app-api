using System;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionTextException : Exception
	{
		public EmptyQuestionTextException() : base("Question text can not be empty.")
		{
		}
	}
}
