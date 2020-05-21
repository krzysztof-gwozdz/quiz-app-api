using QuizApp.Application.Dtos;
using System;

namespace QuizApp.Application.Tests.Examples
{
	public static class CreateAnswerDtoExample
	{
		public static CreateAnswerDto ValidCorrectAnswerDto =>
			new CreateAnswerDto { Text = Guid.NewGuid().ToString(), IsCorrect = true };

		public static CreateAnswerDto ValidInCorrectAnswerDto =>
			new CreateAnswerDto { Text = Guid.NewGuid().ToString(), IsCorrect = false };
	}
}
