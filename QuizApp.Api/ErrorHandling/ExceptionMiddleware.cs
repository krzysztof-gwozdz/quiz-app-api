using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace QuizApp.Api.ErrorHandling
{
	public class ExceptionMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IExceptionToResponseMapper _exceptionToResponseMapper;

		public ExceptionMiddleware(RequestDelegate next, IExceptionToResponseMapper exceptionToResponseMapper)
		{
			_next = next;
			_exceptionToResponseMapper = exceptionToResponseMapper;
		}

		public async Task InvokeAsync(HttpContext httpContext)
		{
			try
			{
				await _next(httpContext);
			}
			catch (Exception exception)
			{
				var response = _exceptionToResponseMapper.Map(exception);
				var json = JsonConvert.SerializeObject(new { response.Code, response.Message });
				httpContext.Response.StatusCode = (int)response.HttpStatusCode;
				httpContext.Response.ContentType = "application/json";
				await httpContext.Response.WriteAsync(json);
			}
		}
	}
}
