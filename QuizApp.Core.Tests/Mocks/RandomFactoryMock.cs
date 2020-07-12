using QuizApp.Core.Factories;
using System;

namespace QuizApp.Core.Tests.Mocks
{
	public class RandomFactoryMock : IRandomFactory
	{
		private readonly int[] _intSequence;
		private readonly double[] _doubleSequence;
		private int _intIndex = 0;
		private int _doubleIndex = 0;

		public RandomFactoryMock(int[] intSequence)
			=> _intSequence = intSequence;

		public RandomFactoryMock(double[] doubleSequence)
			=> _doubleSequence = doubleSequence;

		public RandomFactoryMock(int[] intSequence, double[] doubleSequence)
			=> (_intSequence, _doubleSequence) = (intSequence, doubleSequence);

		public int NextInt() => _intSequence?[_intIndex++] ?? throw new ArgumentException("The sequence must be initialized before use.", nameof(_intSequence));
		public int NextInt(int maxValue) => _intSequence?[_intIndex++] ?? throw new ArgumentException("The sequence must be initialized before use.", nameof(_intSequence));
		public double NextDouble() => _doubleSequence?[_doubleIndex++] ?? throw new ArgumentException("The sequence must be initialized before use.", nameof(_doubleSequence));
	}
}
