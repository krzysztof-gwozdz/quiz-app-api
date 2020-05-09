﻿using System;

namespace QuizApp.Dtos
{
	public class Question
	{
		public Guid Id { get; set; }
		public string Text { get; set; }
		public Answer[] Answers{ get; set; }
		public Guid CorrectAnswerId { get; set; }
	}
}
