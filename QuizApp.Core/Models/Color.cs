using QuizApp.Shared.Exceptions;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace QuizApp.Core.Models
{
	public class Color
	{
		private const string Pattern = "^#(?:[0-9a-fA-F]{3}){1,2}$";

		public string Value { get; }

		public Color(string value)
		{
			Value = value;
		}

		public static Color Create(string value)
		{
			Validate(value);
			return new Color(value);
		}

		public static void Validate(string value)
		{
			var errors = new HashSet<ValidationError>();

			if (!Regex.IsMatch(value ?? string.Empty, Pattern))
				errors.Add(new ValidationError("color", $"Color: {value} is invalid."));

			if (errors.Any())
				throw new ValidationException(errors.ToArray());
		}
	}
}