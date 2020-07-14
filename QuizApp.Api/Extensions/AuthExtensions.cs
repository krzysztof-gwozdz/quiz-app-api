using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using QuizApp.Application;
using QuizApp.Shared;

namespace QuizApp.Api.Extensions
{
	public static class AuthExtensions
	{
		public static IServiceCollection AddAuth(this IServiceCollection services)
		{
			var options = services.GetOptions<AuthOptions>("Auth");
			services.AddSingleton(options);
			services.AddAuthentication(o =>
			{
				o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
			.AddJwtBearer(o =>
			{
				o.RequireHttpsMetadata = false;
				o.SaveToken = true;
				o.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(options.Key),
					ValidateIssuer = false,
					ValidateAudience = false
				};
			});
			return services;
		}

		public static IApplicationBuilder UseAuth(this IApplicationBuilder app)
		{
			app.UseAuthentication();
			app.UseAuthorization();
			return app;
		}
	}
}
