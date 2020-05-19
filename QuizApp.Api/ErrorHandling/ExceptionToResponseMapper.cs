using QuizApp.Shared.Exceptions;
using System;
using System.Net;

namespace QuizApp.Api.ErrorHandling
{
	public class ExceptionToResponseMapper : IExceptionToResponseMapper
	{
		public ExceptionResponse Map(Exception exception) =>
			exception switch
			{
				NotFoundException ex => new ExceptionResponse(ex.Code, ex.Message, HttpStatusCode.NotFound),

				DomainException ex => new ExceptionResponse(ex.Code, ex.Message, HttpStatusCode.BadRequest),
				AppException ex => new ExceptionResponse(ex.Code, ex.Message, HttpStatusCode.BadRequest),
				InfrastructureException ex => new ExceptionResponse(ex.Code, ex.Message, HttpStatusCode.InternalServerError),

				NotImplementedException ex => new ExceptionResponse("error", exception.Message, HttpStatusCode.NotImplemented),
				_ => new ExceptionResponse("error", exception.Message, HttpStatusCode.InternalServerError)
			};
	}
}
