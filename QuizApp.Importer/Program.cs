using QuizApp.Importer.GoogleSheets.Questions;
using QuizApp.Importer.GoogleSheets.Tags;
using System.Threading.Tasks;

namespace QuizApp.Importer
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var logger = new ConsoleLogger();
			await new TagImporter(logger)
				.Import(
					"",
					"https://localhost:44343/tags"
				);
			await new QuestionImporter(logger)
				.Import(
					"",
					"https://localhost:44343/questions"
				);
		}
	}
}
