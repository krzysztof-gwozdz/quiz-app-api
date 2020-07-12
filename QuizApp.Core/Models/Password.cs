using QuizApp.Core.Exceptions;
using System.Text.RegularExpressions;

namespace QuizApp.Core.Models
{
	public class Password
	{
		public const byte MinimumLength = 8;
		public const byte MaximumLength = 64;
		public const string AllowedSpecialCharacters = @"~`!@#$%^&*()+=_\-\|:;”’?\/<>,.";
		public static readonly string Pattern = $"^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[{AllowedSpecialCharacters}])[A-Za-z\\d{AllowedSpecialCharacters}]" + "{" + MinimumLength + "," + MaximumLength + "}$";

		public string Value { get; }

		public Password(string value)
		{
			Value = value;
		}

		public static Password Create(string value)
		{
			var regex = new Regex(Pattern);
			if (value is null || !regex.IsMatch(value))
				throw new InvalidPasswordException();

			return new Password(value);
		}
	}
}
