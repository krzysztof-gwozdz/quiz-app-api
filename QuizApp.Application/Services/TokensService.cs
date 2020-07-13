using Microsoft.IdentityModel.Tokens;
using QuizApp.Application.Dtos;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace QuizApp.Application.Services
{
	public class TokensService : ITokensService
	{
		private readonly IdentityOptions _options;

		public TokensService(IdentityOptions options)
		{
			_options = options;
		}

		public TokenDto Create(string username)
		{
			var handler = new JwtSecurityTokenHandler();
			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Issuer = _options.Issuer,
				Subject = new ClaimsIdentity(new[]
				{
					new Claim(ClaimTypes.Name, username),
					new Claim(ClaimTypes.Role, "user"),
				}),
				Expires = DateTime.UtcNow.AddSeconds(_options.ExpirationSeconds),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(_options.Key),
					SecurityAlgorithms.HmacSha256Signature)
			};

			var securityToken = handler.CreateToken(tokenDescriptor);
			var token = new TokenDto
			{
				Token = handler.WriteToken(securityToken),
				Issuer = securityToken.Issuer,
				Subject = username,
				ValidFrom = securityToken.ValidFrom,
				ValidTo = securityToken.ValidTo
			};
			return token;
		}
	}
}
