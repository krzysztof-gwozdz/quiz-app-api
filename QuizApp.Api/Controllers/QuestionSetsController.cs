using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace QuizApp.Api.Controllers
{
	[ApiController]
	[Route("question-sets")]
	public class QuestionSetsController : ControllerBase
	{
		private readonly IQuestionSetsService _questionSetsService;

		public QuestionSetsController(IQuestionSetsService questionSetsService)
		{
			_questionSetsService = questionSetsService;
		}

		[HttpGet("")]
		public async Task<ActionResult<QuestionSetsDto>> Get()
		{
			return Ok(await _questionSetsService.GetCollectionAsync());
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<QuestionSetDto>> Get(Guid id)
		{
			return Ok(await _questionSetsService.GetAsync(id));
		}

		[HttpGet("{id:guid}/icon")]
		public async Task<ActionResult> GetIcon(Guid id)
		{
			var icon = await _questionSetsService.GetIconAsync(id);
			return File(icon, MediaTypeNames.Image.Jpeg);
		}

		[HttpPost("")]
		public async Task<ActionResult> Create([FromForm] CreateQuestionSetDto createQuestionSetDto)
		{
			return Created((await _questionSetsService.CreateAsync(createQuestionSetDto)).ToString(), null);
		}
	}
}
