using System;

namespace QuizApp.Core.Exceptions
{
	public class EmptyCorrectAnswerException : Exception
	{
		public EmptyCorrectAnswerException() : base("Correct answer can not be empty.")
		{
		}
	}
}
