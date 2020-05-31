using QuizApp.Core.Exceptions;
using System;
using System.IO;
using System.Net.Mime;

namespace QuizApp.Core.Models
{
	public class QuestionSetIcon
	{
		public const long MaxImageSize = 1024 * 1024 * 1;

		public Guid Id { get; }
		public Stream Data { get; }

		public QuestionSetIcon(Guid id, Stream data)
		{
			Id = id;
			Data = data;
		}

		private QuestionSetIcon(Stream data)
			: this(Guid.NewGuid(), data)
		{
		}

		public static QuestionSetIcon Create(Stream data, string contentType)
		{
			if (data is null || data.Length == 0)
				throw new EmptyQuestionSetIconException();

			if (data.Length > MaxImageSize)
				throw new QuestionSetIconIsToLargeException(data.Length, MaxImageSize);

			if (contentType != MediaTypeNames.Image.Jpeg)
				throw new InvalidMediaTypeException(contentType, MediaTypeNames.Image.Jpeg);

			//TODO validate image content and add possibility add other extensions

			return new QuestionSetIcon(data);
		}
	}
}