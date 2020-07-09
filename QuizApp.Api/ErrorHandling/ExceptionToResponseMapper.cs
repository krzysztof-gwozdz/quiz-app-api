using QuizApp.Shared.Exceptions;
using System;
using System.Net;

namespace QuizApp.Api.ErrorHandling
{
	public class ExceptionToResponseMapper : IExceptionToResponseMapper
	{
		public ErrorResponse Map(Exception exception) =>
			exception switch
			{
				NotFoundException ex => new ErrorResponse(ex.Code, ex.Message, HttpStatusCode.NotFound),

				DomainException ex => new ErrorResponse(ex.Code, ex.Message, HttpStatusCode.BadRequest),
				AppException ex => new ErrorResponse(ex.Code, ex.Message, HttpStatusCode.BadRequest),
				InfrastructureException ex => new ErrorResponse(ex.Code, ex.Message, HttpStatusCode.InternalServerError),

				NotImplementedException ex => new ErrorResponse("error", ex.Message, HttpStatusCode.NotImplemented),
				_ => new ErrorResponse("error", exception.Message, HttpStatusCode.InternalServerError)
			};
	}
}
