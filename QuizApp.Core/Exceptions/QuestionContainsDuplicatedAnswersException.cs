using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionContainsDuplicatedAnswersException : Exception
	{
		public QuestionContainsDuplicatedAnswersException(string duplicatdAnswer) : base($"Question contains duplicated answers: {duplicatdAnswer}.")
		{
		}
	}
}