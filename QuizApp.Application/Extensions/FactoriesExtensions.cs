using Microsoft.Extensions.DependencyInjection;
using QuizApp.Core.Factories;
using System;

namespace QuizApp.Application.Extensions
{
	public static class FactoriesExtensions
	{
		public static IServiceCollection AddFactories(this IServiceCollection services)
		{
			services.Scan(s =>
				s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
					.AddClasses(c => c.AssignableTo(typeof(IFactory)))
					.AsImplementedInterfaces()
					.WithTransientLifetime());

			return services;
		}
	}
}
