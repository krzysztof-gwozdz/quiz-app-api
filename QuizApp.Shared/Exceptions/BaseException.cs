using System;

namespace QuizApp.Shared.Exceptions
{
	public abstract class BaseException : Exception
	{
		public abstract string Code { get; }

		protected BaseException(string message) : base(message)
		{
		}
	}
}
