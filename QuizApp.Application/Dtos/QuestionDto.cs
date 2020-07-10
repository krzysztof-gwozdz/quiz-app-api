﻿using System;

namespace QuizApp.Application.Dtos
{
	public class QuestionDto
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public AnswerDto[] Answers { get; set; }
		public string[] Tags { get; set; }

		public QuestionDto()
		{
		}

		public QuestionDto(Guid id, string text, AnswerDto[] answers, string[] tags)
		{
			Id = id;
			Text = text;
			Answers = answers;
			Tags = tags;
		}
	}
}
