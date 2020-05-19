using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class QuestionIsNotAPartOfQuizException : DomainException
	{
		public override string Code => "question_is_not_a_part_of_quiz";

		public QuestionIsNotAPartOfQuizException(Guid questionId, Guid quizId) : base($"Question: {questionId} is not a part of quiz: {quizId}.")
		{
		}
	}
}
