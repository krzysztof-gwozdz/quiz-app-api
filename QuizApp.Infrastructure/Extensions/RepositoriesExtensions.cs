using Microsoft.Extensions.DependencyInjection;
using QuizApp.Core.Repositories;
using System;

namespace QuizApp.Api.Extensions
{
	public static class RepositoriesExtensions
	{
		public static IServiceCollection AddRepositories(this IServiceCollection services)
		{
			services.Scan(s =>
				s.FromAssemblies(AppDomain.CurrentDomain.GetAssemblies())
					.AddClasses(c => c.AssignableTo(typeof(IRepository)))
					.AsImplementedInterfaces()
					.WithTransientLifetime());

			return services;
		}
	}
}
