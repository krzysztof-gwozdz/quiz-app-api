using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace QuizApp.Shared
{
	public static class StreamExtensions
	{
		private static readonly string[] JpgTestBytes = { "FF", "D8" };
		private static readonly string[] BmpTestBytes = { "42", "4D" };
		private static readonly string[] GifTestBytes = { "47", "49", "46" };
		private static readonly string[] PngTestBytes = { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
		private static readonly string[][] ImageTypeTestBytes = { JpgTestBytes, BmpTestBytes, GifTestBytes, PngTestBytes };

		private static int LongestTestBytesCount => ImageTypeTestBytes.Max(x => x.Length);

		public static bool IsImage(this Stream stream)
		{
			stream.Seek(0, SeekOrigin.Begin);

			var bytesIterated = new List<string>();

			for (int i = 0; i < LongestTestBytesCount; i++)
			{
				string bit = stream.ReadByte().ToString("X2");
				bytesIterated.Add(bit);

				bool isImage = ImageTypeTestBytes.Any(img => !img.Except(bytesIterated).Any());
				if (isImage)
					return true;
			}

			return false;
		}
	}
}
