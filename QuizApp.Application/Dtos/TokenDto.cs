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

		public TokenDto()
		{
		}

		public TokenDto(string token, string refreshToken, string issuer, string subject, DateTime validFrom, DateTime validTo)
		{
			Token = token;
			RefreshToken = refreshToken;
			Issuer = issuer;
			Subject = subject;
			ValidFrom = validFrom;
			ValidTo = validTo;
		}
	}
}
