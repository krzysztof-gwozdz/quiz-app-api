using FluentAssertions;
using Moq;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using QuizApp.Core.Tests.Examples;
using System;
using System.Threading.Tasks;
using Xunit;

namespace QuizApp.Application.Tests.Services
{
	public class TagsServiceTests
	{
		private readonly Mock<ITagsRepository> _tagsRepositoryMock;
		private readonly TagsService _questionSetsService;

		public TagsServiceTests()
		{
			_tagsRepositoryMock = new Mock<ITagsRepository>();
			_questionSetsService = new TagsService(_tagsRepositoryMock.Object);
		}

		[Fact]
		public async Task GetTags_Tags()
		{
			//arrange
			var tagsCollection = new[] { TagExample.ValidTag, TagExample.ValidTag, TagExample.ValidTag };
			_tagsRepositoryMock
				.Setup(x => x.GetAllAsync())
				.ReturnsAsync(tagsCollection);

			//act 
			var tags = await _questionSetsService.GetCollectionAsync();

			//assert
			tags.Collection.Should().HaveCount(tagsCollection.Length);
		}

		[Fact]
		public async Task CreateTagsWithUniqueName_TagsCreated()
		{
			//arrange
			var name = TagExample.ValidName;
			var dto = new CreateTagDto(name);

			//act 
			var tagId = await _questionSetsService.CreateAsync(dto);

			//assert
			tagId.Should().NotBeEmpty();
		}

		[Fact]
		public async Task CreateTagsWithNameOfAnotherTags_ThrowException()
		{
			//arrange
			var name = TagExample.ValidName;
			_tagsRepositoryMock
				.Setup(x => x.GetByNameAsync(name))
				.ReturnsAsync(new Tag(TagExample.NewId, name));
			var dto = new CreateTagDto(name);

			//act 
			Func<Task> createTags = async () => await _questionSetsService.CreateAsync(dto);

			//assert
			await createTags.Should().ThrowAsync<TagWithSelectedNameAlreadyExistsException>()
				.WithMessage($"Tag with name: {name} already exists.");
		}
	}
}
