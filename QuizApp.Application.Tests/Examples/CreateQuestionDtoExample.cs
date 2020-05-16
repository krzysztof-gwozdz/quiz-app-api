using QuizApp.Application.Dtos;
using QuizApp.Core.Tests.Examples;
using System.Linq;

namespace QuizApp.Application.Tests.Examples
{
	public static class CreateQuestionDtoExample
	{
		public static CreateQuestionDto ValidDto
		{
			get
			{
				var answers = new[]
				{
					CreateAnswerDtoExample.ValidDto,
					CreateAnswerDtoExample.ValidDto,
					CreateAnswerDtoExample.ValidDto,
					CreateAnswerDtoExample.ValidDto
				};
				return new CreateQuestionDto
				{
					Text = QuestionExample.ValidText,
					Answers = answers,
					CorrectAnswer = answers.First().Text,
					QuestionSetId = QuestionSetExample.NewId
				};
			}
		}
	}
}
