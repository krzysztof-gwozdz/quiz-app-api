namespace QuizApp.Shared.Exceptions
{
	public class MissingParameterException : InfrastructureException
	{
		public override string Code => "missing_parameter";

		public MissingParameterException(string parameter) : base($"Missing parameter: {parameter}.")
		{
		}
	}
}
