namespace QuizApp.Infrastructure.Options
{
	public class CosmosDbOptions
	{
		public ConnectionStringMode Mode { get; set; }
		public ConnectionString Azure { get; set; }
		public ConnectionString Emulator { get; set; }
		public string DatabaseName { get; set; }
		public ConnectionString ConnectionString =>
			Mode == ConnectionStringMode.Azure ? Azure : Emulator;
	}

	public enum ConnectionStringMode
	{
		Azure,
		Emulator
	}

	public class ConnectionString
	{
		public string ServiceEndpoint { get; set; }
		public string AuthKey { get; set; }
	}
}
