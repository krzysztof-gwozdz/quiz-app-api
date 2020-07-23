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
	[Route("quizzes")]
	public class QuizzesController : ControllerBase
	{
		private readonly IQuizzesService _quizService;

		public QuizzesController(IQuizzesService quizService)
		{
			_quizService = quizService;
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuizDto))]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<QuizDto>> Get(Guid id)
		{
			return Ok(await _quizService.GetAsync(id));
		}

		[HttpGet("{id:guid}/summary")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuizSummaryDto))]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<QuizSummaryDto>> GetSummary(Guid id)
		{
			return Ok(await _quizService.GetSummaryAsync(id));
		}

		[HttpPost("")]
		[ProducesResponseType((int)HttpStatusCode.Created)]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult> Generate(QuizParametersDto quizParametersDto)
		{
			return Created((await _quizService.GenerateAsync(quizParametersDto)).ToString(), null);
		}

		[HttpPatch("{id:guid}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult> Solve(Guid id, SolvedQuizDto solvedQuizDto)
		{
			solvedQuizDto.QuizId = id;
			await _quizService.SolveAsync(solvedQuizDto);
			return Ok();
		}
	}
}