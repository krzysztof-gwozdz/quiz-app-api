namespace QuizApp.Importer
{
	internal interface ILogger
	{
		void LogSuccess(string message);
		void LogError(string message);
	}
}
