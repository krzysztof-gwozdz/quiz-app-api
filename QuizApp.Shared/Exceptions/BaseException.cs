using System;

namespace QuizApp.Shared.Exceptions
{
	public abstract class BaseException : Exception
	{
		public abstract string Code { get; }

		public BaseException(string message) : base(message)
		{
		}
	}
}
