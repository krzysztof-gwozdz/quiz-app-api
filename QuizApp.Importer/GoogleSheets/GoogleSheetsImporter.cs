using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace QuizApp.Importer.GoogleSheets
{
	internal abstract class GoogleSheetsImporter<RowType, DtoType>
	{
		private readonly ILogger _logger;

		protected GoogleSheetsImporter(ILogger logger)
		{
			_logger = logger;
		}

		public async Task Import(string googleSheetPath, string apiUrl)
		{
			await CallApi(await Get(googleSheetPath), apiUrl);
		}

		protected async Task CallApi(DtoType[] dtos, string apiUrl)
		{
			using (var httpClient = new HttpClient())
			{
				foreach (var dto in dtos)
				{
					var jsonObject = JsonSerializer.Serialize(dto);
					var content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");
					var response = await httpClient.PostAsync(apiUrl, content);
					if (response.IsSuccessStatusCode)
						_logger.LogSuccess($"OK ({response.StatusCode}) - '{dto}' added");
					else
						_logger.LogError($"ERROR ({response.StatusCode}) - '{dto}' not added.");
				}
			}
		}

		protected async Task<DtoType[]> Get(string googleSheetPath)
			=> MapResults(Filter(GetRows(await LaodGoogleSheet(googleSheetPath))));

		protected virtual async Task<Stream> LaodGoogleSheet(string googleSheetPath)
		{
			using (var httpClient = new HttpClient())
			{
				var httpResponse = await httpClient.GetAsync(googleSheetPath);
				return await httpResponse.Content.ReadAsStreamAsync();
			}
		}

		protected abstract RowType[] GetRows(Stream googleSheet);

		protected virtual RowType[] Filter(RowType[] rows)
			=> rows;

		protected abstract DtoType[] MapResults(RowType[] rows);
	}
}