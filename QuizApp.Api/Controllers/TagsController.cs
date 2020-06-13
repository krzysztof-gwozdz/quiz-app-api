using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using System.Threading.Tasks;

namespace QuizApp.Api.Controllers
{
	[ApiController]
	[Route("tags")]
	public class TagsController : ControllerBase
	{
		private readonly ITagsService _tagsService;

		public TagsController(ITagsService tagsService)
		{
			_tagsService = tagsService;
		}

		[HttpGet("")]
		public async Task<ActionResult<TagsDto>> Get()
		{
			var tags = await _tagsService.GetCollectionAsync();
			return Ok(tags);
		}

		[HttpPost("")]
		public async Task<ActionResult> Create(CreateTagDto createTagDto)
		{
			return Created((await _tagsService.CreateAsync(createTagDto)).ToString(), null);
		}
	}
}