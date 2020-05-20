using Microsoft.AspNetCore.Mvc;
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
		public async Task<ActionResult<QuizDto>> Get(Guid id)
		{
			return Ok(await _quizService.GetAsync(id));
		}

		[HttpGet("{id:guid}/summary")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuizSummaryDto))]
		public async Task<ActionResult<QuizSummaryDto>> GetSummary(Guid id)
		{
			return Ok(await _quizService.GetSummaryAsync(id));
		}

		[HttpPost("")]
		[ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(Guid))]
		public async Task<ActionResult<Guid>> Generate(QuizParametersDto quizParameters)
		{
			return Created((await _quizService.GenerateAsync(quizParameters)).ToString(), null);
		}

		[HttpPut("{id:guid}")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult> Solve(Guid id, SolvedQuizDto solvedQuiz)
		{
			solvedQuiz.QuizId = id;
			await _quizService.SolveAsync(solvedQuiz);
			return Ok();
		}
	}
}