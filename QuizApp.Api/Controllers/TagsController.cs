using Microsoft.AspNetCore.Mvc;
using QuizApp.Api.ErrorHandling;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using System.Net;
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
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TagsDto))]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<TagsDto>> Get()
		{
			var tags = await _tagsService.GetCollectionAsync();
			return Ok(tags);
		}

		[HttpGet("{name}")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TagDto))]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<TagDto>> Get(string name)
		{
			var tag = await _tagsService.GetAsync(name);
			return Ok(tag);
		}

		[HttpPost("")]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult> Create(CreateTagDto createTagDto)
		{
			return Created((await _tagsService.CreateAsync(createTagDto)).ToString(), null);
		}
	}
}