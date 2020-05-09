using Microsoft.AspNetCore.Mvc;
using QuizApp.Dtos;
using QuizApp.Services;
using System;
using System.Threading.Tasks;

namespace QuizApp.Controllers
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

		[HttpGet("{id:guid}", Name = nameof(Get))]
		public async Task<ActionResult<Quiz>> Get(Guid id)
		{
			return Ok(await _quizService.Get(id));
		}

		[HttpGet("{id:guid}/summary")]
		public async Task<ActionResult<QuizSummary>> GetSummary(Guid id)
		{
			return Ok(await _quizService.GetSummary(id));
		}

		[HttpPost("")]
		public async Task<ActionResult<Guid>> Generate(QuizParameters quizParameters)
		{
			return CreatedAtAction(nameof(Get), new { id = await _quizService.Generate(quizParameters) }, null);
		}

		[HttpPut("")]
		public async Task<ActionResult> Solve(SolvedQuiz solvedQuiz)
		{
			await _quizService.Solve(solvedQuiz);
			return Ok();
		}
	}
}