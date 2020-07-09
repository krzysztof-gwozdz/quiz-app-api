using FluentAssertions;
using NSubstitute;
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
		private readonly ITagsRepository _tagsRepository;
		private readonly TagsService _tagsService;

		public TagsServiceTests()
		{
			_tagsRepository = Substitute.For<ITagsRepository>();
			_tagsService = new TagsService(_tagsRepository);
		}

		[Fact]
		public async Task GetTags_Tags()
		{
			//arrange
			var tagsCollection = new[] { TagExample.ValidTag, TagExample.ValidTag, TagExample.ValidTag };
			_tagsRepository.GetAllAsync().Returns(tagsCollection);

			//act 
			var tags = await _tagsService.GetCollectionAsync();

			//assert
			tags.Collection.Should().HaveCount(tagsCollection.Length);
		}

		[Fact]
		public async Task CreateTagsWithUniqueName_TagsCreated()
		{
			//arrange
			var name = TagExample.ValidName;
			var description = TagExample.ValidDescription;
			var dto = new CreateTagDto(name, description);

			//act 
			var tagId = await _tagsService.CreateAsync(dto);

			//assert
			tagId.Should().NotBeEmpty();
		}

		[Fact]
		public async Task CreateTagsWithNameOfAnotherTags_ThrowException()
		{
			//arrange
			var name = TagExample.ValidName;
			var description = TagExample.ValidDescription;
			_tagsRepository.GetByNameAsync(name).Returns(new Tag(name, description));
			var dto = new CreateTagDto(name, description);

			//act 
			Func<Task> createTags = async () => await _tagsService.CreateAsync(dto);

			//assert
			await createTags.Should().ThrowAsync<TagWithSelectedNameAlreadyExistsException>()
				.WithMessage($"Tag with name: {name} already exists.");
		}
	}
}
