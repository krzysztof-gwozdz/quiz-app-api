using QuizApp.Application.Dtos;
using QuizApp.Core.Tests.Examples;

namespace QuizApp.Application.Tests.Examples
{
	public static class CreateQuestionDtoExample
	{
		public static CreateQuestionDto ValidDto =>
			new CreateQuestionDto
			{
				Text = QuestionExample.ValidText,
				Answers = new[]
				{
					CreateAnswerDtoExample.ValidCorrectAnswerDto,
					CreateAnswerDtoExample.ValidInCorrectAnswerDto,
					CreateAnswerDtoExample.ValidInCorrectAnswerDto,
					CreateAnswerDtoExample.ValidInCorrectAnswerDto
				},
				QuestionSetId = QuestionSetExample.NewId
			};
	}
}
