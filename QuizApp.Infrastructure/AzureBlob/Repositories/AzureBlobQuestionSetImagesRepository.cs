using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Infrastructure.AzureBlob.Core;
using System;
using System.Threading.Tasks;

namespace QuizApp.Infrastructure.AzureBlob.Repositories
{
	[AzureBlobRepository("question-set-images")]
	internal class AzureBlobQuestionSetImagesRepository : AzureBlobRepository, IQuestionSetImagesRepository
	{
		public AzureBlobQuestionSetImagesRepository(IAzureBlobClientFactory factory) : base(factory)
		{
		}

		public async Task<QuestionSetImage> GetAsync(Guid id)
		{
			var blob = await GetBlobAsync(id.ToString());
			return new QuestionSetImage(id, blob.Content, blob.ContentType);
		}

		public async Task<bool> Exists(Guid id) =>
			await CheckIfBlobExists(id.ToString());

		public async Task AddAsync(QuestionSetImage image) =>
			await AddBlobAsync(image.Id.ToString(), image.Data, image.ContentType);
	}
}
