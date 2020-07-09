using System.Net;

namespace QuizApp.Api.ErrorHandling
{
	public class ErrorResponse
	{
		public string Code { get; }
		public string Message { get; }
		public HttpStatusCode HttpStatusCode { get; }

		public ErrorResponse(string code, string message, HttpStatusCode httpStatusCode) =>
			(Code, Message, HttpStatusCode) = (code, message, httpStatusCode);
	}
}