using Microsoft.Extensions.DependencyInjection;
using QuizApp.Application.Services;
using System;

namespace QuizApp.Application.Extensions
{
	public static class ServicesExtensions
	{
		public static IServiceCollection AddServices(this IServiceCollection services)
		{
			services.Scan(s =>
				s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
					.AddClasses(c => c.AssignableTo(typeof(IService)))
					.AsImplementedInterfaces()
					.WithTransientLifetime());

			return services;
		}
	}
}
