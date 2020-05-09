using QuizApp.Models;
using System.Threading.Tasks;

namespace QuizApp.Services
{
	public interface IQuestionSetService
	{
		Task<QuestionSet[]> GetCollection();
	}
}