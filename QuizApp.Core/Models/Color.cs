using QuizApp.Core.Exceptions;
using System.Text.RegularExpressions;

namespace QuizApp.Core.Models
{
	public class Color
	{
		private const string Patter = "^#(?:[0-9a-fA-F]{3}){1,2}$";

		public string Value { get; }

		public Color(string value)
		{
			Value = value;
		}

		public static Color Create(string value)
		{
			if (!Regex.IsMatch(value, Patter))
				throw new InvalidColorException(value);
			return new Color(value);
		}
	}
}