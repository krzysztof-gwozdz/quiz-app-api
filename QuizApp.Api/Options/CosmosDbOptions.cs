using System;
using System.Collections.Generic;
using System.Linq;

namespace QuizApp.Api.Options
{
	public class CosmosDbOptions
	{
		public ConnectionStringMode Mode { get; set; }
		public ConnectionString Azure { get; set; }
		public ConnectionString Emulator { get; set; }
		public string DatabaseName { get; set; }
		public List<CollectionInfo> Collections { get; set; }

		public ConnectionString ConnectionString =>
			Mode == ConnectionStringMode.Azure ? Azure : Emulator;
		public List<string> CollectionNames =>
			Collections?.Select(x => x.Name).ToList();
	}

	public enum ConnectionStringMode
	{
		Azure,
		Emulator
	}

	public class ConnectionString
	{
		public Uri ServiceEndpoint { get; set; }
		public string AuthKey { get; set; }
	}

	public class CollectionInfo
	{
		public string Name { get; set; }
		public string PartitionKey { get; set; }
	}
}
