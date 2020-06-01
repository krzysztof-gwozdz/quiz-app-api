﻿using QuizApp.Core.Exceptions;
using System;
using System.IO;
using System.Linq;
using System.Net.Mime;
using QuizApp.Shared;

namespace QuizApp.Core.Models
{
	public class QuestionSetIcon
	{
		public const long MaxImageSize = 1024 * 1024 * 1;

		public static readonly string[] ValidMediaTypes = { MediaTypes.Image.Jpeg, MediaTypes.Image.Png, MediaTypes.Image.Gif };

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

			if (!ValidMediaTypes.Contains(contentType))
				throw new InvalidMediaTypeException(contentType, ValidMediaTypes);

			//TODO validate image content and add possibility add other extensions

			return new QuestionSetIcon(data);
		}
	}
}