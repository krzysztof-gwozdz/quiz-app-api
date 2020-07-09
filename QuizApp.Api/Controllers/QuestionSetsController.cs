using Microsoft.AspNetCore.Mvc;
using QuizApp.Api.ErrorHandling;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using System;
using System.Net;
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
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuestionSetsDto))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<QuestionSetsDto>> Get()
		{
			var questions = await _questionSetsService.GetCollectionAsync();
			foreach (var questionSet in questions.Collection)
				questionSet.ImageUrl = GetImageUrl(questionSet.Id);
			return Ok(questions);
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuestionSetDto))]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<QuestionSetDto>> Get(Guid id)
		{
			var questionSet = await _questionSetsService.GetAsync(id);
			questionSet.ImageUrl = GetImageUrl(id);

			return Ok(questionSet);
		}

		[HttpGet("{id:guid}/image")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<FileStreamResult> GetImage(Guid id)
		{
			var image = await _questionSetsService.GetImageAsync(id);
			return File(image.Data, image.ContentType);
		}

		[HttpPost("")]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult> Create([FromForm] CreateQuestionSetDto createQuestionSetDto)
		{
			return Created((await _questionSetsService.CreateAsync(createQuestionSetDto)).ToString(), null);
		}

		private string GetImageUrl(Guid id) =>
			Url.ActionLink(nameof(GetImage), "QuestionSets", new { id });
	}
}
