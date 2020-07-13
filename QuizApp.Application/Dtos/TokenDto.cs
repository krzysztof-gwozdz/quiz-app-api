using System;

namespace QuizApp.Application.Dtos
{
	public class TokenDto
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }
		public string Issuer { get; set; }
		public string Subject { get; set; }
		public DateTime ValidFrom { get; set; }
		public DateTime ValidTo { get; set; }
	}
}
