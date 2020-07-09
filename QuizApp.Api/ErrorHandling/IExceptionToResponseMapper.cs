using System;

namespace QuizApp.Api.ErrorHandling
{
	public interface IExceptionToResponseMapper
	{
		ErrorResponse Map(Exception exception);
	}
}
