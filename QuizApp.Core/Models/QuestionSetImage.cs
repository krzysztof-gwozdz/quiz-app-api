using QuizApp.Core.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using QuizApp.Shared;

namespace QuizApp.Core.Models
{
	public class QuestionSetImage
	{
		public const long MaxImageSize = 1024 * 1024 * 1;

		public static readonly string[] ValidMediaTypes = { MediaTypes.Image.Jpeg, MediaTypes.Image.Png, MediaTypes.Image.Gif };

		public Guid Id { get; }
		public Stream Data { get; }

		public QuestionSetImage(Guid id, Stream data)
		{
			Id = id;
			Data = data;
		}

		private QuestionSetImage(Stream data)
			: this(Guid.NewGuid(), data)
		{
		}

		public static QuestionSetImage Create(Stream data, string contentType)
		{
			if (data is null || data.Length == 0)
				throw new EmptyQuestionSetImageException();

			if (data.Length > MaxImageSize)
				throw new QuestionSetImageIsToLargeException(data.Length, MaxImageSize);

			if (!ValidMediaTypes.Contains(contentType))
				throw new InvalidMediaTypeException(contentType, ValidMediaTypes);

			if (!data.IsImage())
				throw new UploadedDataIsNotImageException();

			return new QuestionSetImage(data);
		}
	}
}