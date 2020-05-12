using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class FakeQuizesRepository : IQuizesRepository
	{
		public async Task<Quiz> GetAsync(Guid id) => new Quiz
			{
				Id = id,
			};
	}
}
