using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using System;
using System.Net;
using System.Threading.Tasks;

namespace QuizApp.Api.Controllers
{
	[ApiController]
	[Route("quizes")]
	public class QuizesController : ControllerBase
	{
		private IQuizService _quizService;

		public QuizesController(IQuizService quizService)
		{
			_quizService = quizService;
		}

		[HttpGet("{id:guid}")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuizDto))]
		public async Task<ActionResult<QuizDto>> Get(Guid id)
		{
			return Ok(await _quizService.Get(id));
		}

		[HttpGet("{id:guid}/summary")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuizSummaryDto))]
		public async Task<ActionResult<QuizSummaryDto>> GetSummary(Guid id)
		{
			return Ok(await _quizService.GetSummary(id));
		}

		[HttpPost("")]
		[ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(Guid))]
		public async Task<ActionResult<Guid>> Generate(QuizParametersDto quizParameters)
		{
			return Created((await _quizService.Generate(quizParameters)).ToString(), null);
		}

		[HttpPut("")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult> Solve(SolvedQuizDto solvedQuiz)
		{
			await _quizService.Solve(solvedQuiz);
			return Ok();
		}
	}
}