namespace QuizApp.Shared
{
	public static class ContentTypes
	{
		public static class Application
		{
			public const string TypeName = "application/";

			public const string Json = TypeName + "json";
			public const string Xml = TypeName + "xml";
			public const string Pdf = TypeName + "pdf";
			public const string Zip = TypeName + "zip";
		}

		public static class Image
		{
			public const string TypeName = "image/";

			public const string Gif = TypeName + "gif";
			public const string Jpeg = TypeName + "jpeg";
			public const string Png = TypeName + "png";
		}

		public static class Text
		{
			public const string TypeName = "text/";

			public const string Plain = TypeName + "plain";
			public const string Html = TypeName + "html";
			public const string Css = TypeName + "css";
			public const string Csv = TypeName + "csv";
			public const string Xml = TypeName + "xml";
		}
	}
}
