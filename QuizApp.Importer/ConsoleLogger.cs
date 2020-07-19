using System;
using static System.Console;

namespace QuizApp.Importer
{
	public class ConsoleLogger : ILogger
	{
		public void LogSuccess(string message)
		{
			ForegroundColor = ConsoleColor.Green;
			WriteLine(message);
			ResetColor();
		}

		public void LogError(string message)
		{
			ForegroundColor = ConsoleColor.Red;
			WriteLine(message);
			ResetColor();
		}
	}
}
