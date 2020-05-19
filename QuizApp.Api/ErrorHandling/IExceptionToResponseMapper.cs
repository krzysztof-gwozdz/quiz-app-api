using System;

namespace QuizApp.Api.ErrorHandling
{
	public interface IExceptionToResponseMapper
	{
		ExceptionResponse Map(Exception exception);
	}
}
