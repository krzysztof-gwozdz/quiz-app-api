using QuizApp.Core.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Factories
{
	public interface IQuestionsFactory
	{
		Task<Quiz.Question[]> GetAsync(Guid questionSetId, int questionCount);
	}
}