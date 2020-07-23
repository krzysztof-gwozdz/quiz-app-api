using QuizApp.Application.Dtos;
using System;

namespace QuizApp.Application.Tests.Examples
{
	public static class EditAnswerDtoExample
	{
		public static Guid ValidId =>
			Guid.NewGuid();

		public static string ValidText =>
			Guid.NewGuid().ToString();

		public static EditAnswerDto ValidCorrectAnswerDto =>
			new EditAnswerDto(ValidId, ValidText, true);

		public static EditAnswerDto ValidInCorrectAnswerDto =>
			new EditAnswerDto(ValidId, ValidText, false);
	}
}
