using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionSetWithSelectedNameAlreadyExistsException : Exception
	{
		public QuestionSetWithSelectedNameAlreadyExistsException(string name) : base($"Question set with name: {name} already exists.")
		{
		}
	}
}
