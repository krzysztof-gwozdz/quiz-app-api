using Microsoft.AspNetCore.Mvc;
using QuizApp.Api.Dtos;
using QuizApp.Api.Services;
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
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Quiz))]
		public async Task<ActionResult<Quiz>> Get(Guid id)
		{
			return Ok(await _quizService.Get(id));
		}

		[HttpGet("{id:guid}/summary")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(QuizSummary))]
		public async Task<ActionResult<QuizSummary>> GetSummary(Guid id)
		{
			return Ok(await _quizService.GetSummary(id));
		}

		[HttpPost("")]
		[ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(Guid))]
		public async Task<ActionResult<Guid>> Generate(QuizParameters quizParameters)
		{
			return Created((await _quizService.Generate(quizParameters)).ToString(), null);
		}

		[HttpPut("")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		public async Task<ActionResult> Solve(SolvedQuiz solvedQuiz)
		{
			await _quizService.Solve(solvedQuiz);
			return Ok();
		}
	}
}