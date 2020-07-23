using QuizApp.Application.Dtos;
using System;

namespace QuizApp.Application.Tests.Examples
{
	public static class EditQuestionDtoExample
	{
		public static Guid ValidId =>
			Guid.NewGuid();

		public static string ValidText =>
			Guid.NewGuid().ToString();

		public static EditAnswerDto[] ValidAnswers =>
			new[]
			{
				EditAnswerDtoExample.ValidCorrectAnswerDto,
				EditAnswerDtoExample.ValidInCorrectAnswerDto,
				EditAnswerDtoExample.ValidInCorrectAnswerDto,
				EditAnswerDtoExample.ValidInCorrectAnswerDto
			};

		public static string[] ValidTags =>
			new[] { Guid.NewGuid().ToString() };

		public static EditQuestionDto ValidDto =>
			new EditQuestionDto
			(
				ValidId,
				ValidText,
				ValidAnswers,
				ValidTags
			);

		public static EditQuestionDto GetValidDto(Guid id) =>
			new EditQuestionDto
			(
				id,
				ValidText,
				ValidAnswers,
				ValidTags
			);
	}
}
