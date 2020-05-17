using QuizApp.Core.Models;
using System;
using System.Threading.Tasks;

namespace QuizApp.Core.Factories
{
	public interface IQuizFactory : IFactory
	{
		Task<Quiz> GetAsync(Guid questionSetId, int questionCount);
	}
}