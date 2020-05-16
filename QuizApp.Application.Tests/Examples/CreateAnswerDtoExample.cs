using QuizApp.Application.Dtos;
using System;

namespace QuizApp.Application.Tests.Examples
{
	public static class CreateAnswerDtoExample
	{
		public static CreateAnswerDto ValidDto =>
			new CreateAnswerDto { Text = Guid.NewGuid().ToString() };
	}
}
