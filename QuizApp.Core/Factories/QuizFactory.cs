using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Shared.Exceptions;
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
			await Validate(questionSetId, questionCount);
			var questions = await GetQuestionsAsync(questionSetId, questionCount);
			return new Quiz(Guid.NewGuid(), questions);
		}

		private async Task Validate(Guid questionSetId, int questionCount)
		{
			var errors = new HashSet<ValidationError>();

			if (!await _questionSetsRepository.ExistsAsync(questionSetId))
				throw new QuestionSetNotFoundException(questionSetId);

			if (questionCount < MinQuestionCount)
				errors.Add(new ValidationError(nameof(questionCount), $"Not enough question: {questionCount}. Min question count: {MinQuestionCount}."));

			var maxQuestionCount = await _questionsRepository.CountBySetIdAsync(questionSetId);
			if (questionCount > maxQuestionCount)
				errors.Add(new ValidationError(nameof(questionCount), $"Too many questions: {questionCount}. Max question count for this question set: {maxQuestionCount}."));

			if (errors.Any())
				throw new ValidationException(errors.ToArray());
		}

		private async Task<HashSet<Quiz.Question>> GetQuestionsAsync(Guid questionSetId, int questionCount)
		{
			var allQuestions = (await _questionsRepository.GetAllBySetIdAsync(questionSetId)).ToList();
			var questions = new List<Quiz.Question>();
			for (int i = 0; i < questionCount; i++)
			{
				int index = _randomFactory.NextInt(allQuestions.Count);
				questions.Add(new Quiz.Question(allQuestions[index]));
				allQuestions.RemoveAt(index);
			}
			return questions.ToHashSet();
		}
	}
}