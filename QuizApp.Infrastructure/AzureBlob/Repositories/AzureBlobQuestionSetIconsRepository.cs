using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.AzureBlob.Core;
using System;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.AzureBlob.Repositories
{
	[AzureBlobRepository("question-sets-icons")]
	public class AzureBlobQuestionSetIconsRepository : AzureBlobRepository, IQuestionSetIconsRepository
	{
		public AzureBlobQuestionSetIconsRepository(IAzureBlobClientFactory factory) : base(factory)
		{
		}

		public async Task<QuestionSetIcon> GetAsync(Guid id) =>
			new QuestionSetIcon(id, await GetBlobAsync(id.ToString()));

		public async Task<bool> Exists(Guid id) =>
			await CheckIfBlobExists(id.ToString());

		public async Task AddAsync(QuestionSetIcon icon) =>
			await AddBlobAsync(icon.Id.ToString(), icon.Data);
	}
}
