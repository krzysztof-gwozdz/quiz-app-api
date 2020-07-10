using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuizApp.Core.Factories
{
	public class QuizFactory : IQuizFactory
	{
		private readonly IQuestionsRepository _questionsRepository;
		private readonly IQuestionSetsRepository _questionSetsRepository;
		private readonly IRandomFactory _randomFactory;

		public const int MinQuestionCount = 2;

		public QuizFactory(
			IQuestionsRepository questionsRepository,
			IQuestionSetsRepository questionSetsRepository,
			IRandomFactory randomFactory)
		{
			_questionsRepository = questionsRepository;
			_questionSetsRepository = questionSetsRepository;
			_randomFactory = randomFactory;
		}

		public async Task<Quiz> GetAsync(Guid questionSetId, int questionCount)
		{
			await ValidateAsync(questionSetId, questionCount);
			var questions = await GetQuestionsAsync(questionSetId, questionCount);
			return new Quiz(Guid.NewGuid(), questionSetId, questions);
		}

		private async Task ValidateAsync(Guid questionSetId, int questionCount)
		{
			if (questionCount < MinQuestionCount)
				throw new NotEnoughQuestionsException(questionCount, MinQuestionCount);

			var questionSet = await _questionSetsRepository.GetByIdAsync(questionSetId);
			if (questionSet is null)
				throw new QuestionSetNotFoundException(questionSetId);

			var maxQuestionCount = await _questionsRepository.CountByTagsAsync(questionSet.Tags);
			if (questionCount > maxQuestionCount)
				throw new TooManyQuestionsException(questionCount, maxQuestionCount);
		}

		private async Task<HashSet<Quiz.Question>> GetQuestionsAsync(Guid questionSetId, int questionCount)
		{
			var allQuestions = await GetAllQuestionsAsync(questionSetId);
			var questions = new List<Quiz.Question>();
			for (int i = 0; i < questionCount; i++)
			{
				int index = _randomFactory.NextInt(allQuestions.Count);
				var question = new Quiz.Question(allQuestions[index]);
				questions.Add(question);
				allQuestions.RemoveAt(index);
			}
			return questions.ToHashSet();
		}

		private async Task<List<Question>> GetAllQuestionsAsync(Guid questionSetId)
		{
			var questionSet = await _questionSetsRepository.GetByIdAsync(questionSetId);
			return (await _questionsRepository.GetAllByTagsAsync(questionSet.Tags)).ToList();
		}
	}
}