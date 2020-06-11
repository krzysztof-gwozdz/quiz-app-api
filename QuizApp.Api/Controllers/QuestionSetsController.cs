using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using System;
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
			var questions = await _questionSetsService.GetCollectionAsync();
			foreach (var questionSet in questions.Collection)
				questionSet.ImageUrl = GetImageUrl(questionSet.Id);
			return Ok(questions);
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<QuestionSetDto>> Get(Guid id)
		{
			var questionSet = await _questionSetsService.GetAsync(id);
			questionSet.ImageUrl = GetImageUrl(id);

			return Ok(questionSet);
		}

		[HttpGet("{id:guid}/image")]
		public async Task<ActionResult> GetImage(Guid id)
		{
			var image = await _questionSetsService.GetImageAsync(id);
			return File(image.Data, image.ContentType);
		}

		[HttpPost("")]
		public async Task<ActionResult> Create([FromForm] CreateQuestionSetDto createQuestionSetDto)
		{
			return Created((await _questionSetsService.CreateAsync(createQuestionSetDto)).ToString(), null);
		}

		private string GetImageUrl(Guid id) =>
			Url.ActionLink(nameof(GetImage), "QuestionSets", new { id });
	}
}
