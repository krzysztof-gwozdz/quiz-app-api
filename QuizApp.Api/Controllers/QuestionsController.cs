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
	[Route("questions")]
	public class QuestionsController : ControllerBase
	{
		private readonly IQuestionsService _questionsService;

		public QuestionsController(IQuestionsService questionsService)
		{
			_questionsService = questionsService;
		}

		[HttpGet("")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuestionsDto))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<QuestionsDto>> Get()
		{
			var questions = await _questionsService.GetCollectionAsync();
			return Ok(questions);
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuestionDto))]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<QuestionDto>> Get(Guid id)
		{
			return Ok(await _questionsService.GetAsync(id));
		}

		[HttpPost("")]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult> Create(CreateQuestionDto createQuestionDto)
		{
			return Created((await _questionsService.CreateAsync(createQuestionDto)).ToString(), null);
		}

		[HttpDelete("{id:guid}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult> Delete(Guid id)
		{
			await _questionsService.RemoveAsync(id);
			return Ok();
		}
	}
}