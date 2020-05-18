using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionIsNotAPartOfQuizException : Exception
	{
		public QuestionIsNotAPartOfQuizException(Guid questionId, Guid quizId) : base($"Question: {questionId} is not a part of quiz: {quizId}.")
		{
		}
	}
}
