using System;

namespace QuizApp.Core.Factories
{
	public class RandomFactory : IRandomFactory
	{
		private readonly Random _random = new Random();

		public int NextInt() => _random.Next();
		public int NextInt(int maxValue) => _random.Next(maxValue);
		public double NextDouble() => _random.NextDouble();
	}
}
