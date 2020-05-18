using QuizApp.Core.Factories;


namespace QuizApp.Core.Tests.Mocks
{
	public class MockRandomFactory : IRandomFactory
	{
		private readonly int[] _intSequence;
		private int _intIndex = 0;

		public MockRandomFactory(int[] intSequence)
		{
			_intSequence = intSequence;
		}

		public int NextInt(int maxValue) => _intSequence[_intIndex++];
	}
}
