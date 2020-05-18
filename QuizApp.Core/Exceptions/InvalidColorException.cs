using System;

namespace QuizApp.Core.Exceptions
{
	public class InvalidColorException : Exception
	{
		public InvalidColorException(string value) : base($"Color: {value} is invalid.")
		{
		}
	}
}
