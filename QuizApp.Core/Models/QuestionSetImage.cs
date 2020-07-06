using QuizApp.Shared;
using QuizApp.Shared.Exceptions;
using System;
using System.Collections.Generic;
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
			Validate(data, contentType);
			return new QuestionSetImage(data, contentType);
		}

		public static void Validate(Stream data, string contentType)
		{
			var errors = new HashSet<ValidationError>();

			if (data is null || data.Length == 0)
				errors.Add(new ValidationError("image", "Question set image can not be empty."));
			else if (data.Length > MaxImageSize)
				errors.Add(new ValidationError("image", $"Question set image is to large: {data.Length}. Max image size: {MaxImageSize}"));
			else if (!ValidContentTypes.Contains(contentType))
				errors.Add(new ValidationError("image", $"Invalid content type: {contentType}. Expected: {string.Join(", ", ValidContentTypes)}."));
			else if (!data.IsImage())
				errors.Add(new ValidationError("image", "Uploaded data is not an image."));

			if (errors.Any())
				throw new ValidationException(errors.ToArray());
		}
	}
}