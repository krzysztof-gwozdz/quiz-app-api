using System;
using System.Collections.Generic;

namespace QuizApp.Infrastructure.Entities
{
	public class QuizEntity : Entity
	{
		public ISet<QuestionEntity> Questions { get; set; }
	}
}
