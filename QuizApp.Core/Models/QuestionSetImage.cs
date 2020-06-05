using QuizApp.Core.Exceptions;
using QuizApp.Shared;
using System;
using System.IO;
using System.Linq;

namespace QuizApp.Core.Models
{
	public class QuestionSetImage
	{
		public const long MaxImageSize = 1024 * 1024 * 1;

		public static readonly string[] ValidContentTypes =
		{
			ContentTypes.Image.Jpeg,
			ContentTypes.Image.Png,
			ContentTypes.Image.Gif
		};

		public Guid Id { get; }
		public Stream Data { get; }
		public string ContentType { get; }

		public QuestionSetImage(Guid id, Stream data, string contentType)
		{
			Id = id;
			Data = data;
			ContentType = contentType;
		}

		private QuestionSetImage(Stream data, string contentType)
			: this(Guid.NewGuid(), data, contentType)
		{
		}

		public static QuestionSetImage Create(Stream data, string contentType)
		{
			if (data is null || data.Length == 0)
				throw new EmptyQuestionSetImageException();

			if (data.Length > MaxImageSize)
				throw new QuestionSetImageIsToLargeException(data.Length, MaxImageSize);

			if (!ValidContentTypes.Contains(contentType))
				throw new InvalidContentTypeException(contentType, ValidContentTypes);

			if (!data.IsImage())
				throw new UploadedDataIsNotImageException();

			return new QuestionSetImage(data, contentType);
		}
	}
}