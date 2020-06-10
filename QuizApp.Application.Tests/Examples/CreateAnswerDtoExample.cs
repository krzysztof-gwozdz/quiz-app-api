using QuizApp.Application.Dtos;
using System;

namespace QuizApp.Application.Tests.Examples
{
	public static class CreateAnswerDtoExample
	{
		public static CreateAnswerDto ValidCorrectAnswerDto =>
			new CreateAnswerDto(Guid.NewGuid().ToString(), true);

		public static CreateAnswerDto ValidInCorrectAnswerDto =>
			new CreateAnswerDto(Guid.NewGuid().ToString(), false);
	}
}
