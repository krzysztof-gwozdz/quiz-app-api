namespace QuizApp.Core.Factories
{
	public interface IRandomFactory : IFactory
	{
		int NextInt(int maxValue);
	}
}