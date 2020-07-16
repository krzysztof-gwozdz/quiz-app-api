using QuizApp.Application.Dtos;
using System;

namespace QuizApp.Application.Tests.Examples
{
	public static class CreateQuestionDtoExample
	{
		public static string ValidText =>
			Guid.NewGuid().ToString();

		public static CreateAnswerDto[] ValidAnswers =>
			new[]
			{
				CreateAnswerDtoExample.ValidCorrectAnswerDto,
				CreateAnswerDtoExample.ValidInCorrectAnswerDto,
				CreateAnswerDtoExample.ValidInCorrectAnswerDto,
				CreateAnswerDtoExample.ValidInCorrectAnswerDto
			};

		public static string[] ValidTags =>
			new[] { Guid.NewGuid().ToString() };

		public static CreateQuestionDto ValidDto =>
			new CreateQuestionDto
			(
				ValidText,
				ValidAnswers,
				ValidTags
			);
	}
}
