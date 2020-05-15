using System;

namespace QuizApp.Core.Exceptions
{
	public class EmptyQuestionSetNameException : Exception
	{
		public EmptyQuestionSetNameException() : base("Question set name can not be empty.")
		{
		}
	}
}
