using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Application.Factories
{
	public class QuestionsFactory : IQuestionsFactory
	{
		private IQuestionsRepository _questionsRepository;

		public QuestionsFactory(IQuestionsRepository questionsRepository)
		{
			_questionsRepository = questionsRepository;
		}

		public async Task<Question[]> GetAsync(Guid questionSetId, int questionCount)
		{
			var allQuestions = (await _questionsRepository.GetAllBySetIdAsync(questionSetId)).ToArray();
			var questions = new List<Question>();
			var random = new Random();
			for (int i = 0; i < questionCount; i++)
			{
				questions.Add(allQuestions[random.Next(allQuestions.Length)]);
			}
			return questions.ToArray();
		}
	}
}