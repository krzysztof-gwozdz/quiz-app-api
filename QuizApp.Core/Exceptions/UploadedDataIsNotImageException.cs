using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class UploadedDataIsNotImageException : DomainException
	{
		public override string Code => "uploaded_data_is_not_image";

		public UploadedDataIsNotImageException() : base("Uploaded data is not an image.")
		{
		}
	}
}
