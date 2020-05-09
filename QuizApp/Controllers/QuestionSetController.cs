using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Models;
using QuizApp.Services;

namespace QuizApp.Controllers
{
	[ApiController]
	[Route("question-set")]
	public class QuestionSetController : ControllerBase
	{
		private IQuestionSetService _questionSetService;

		public QuestionSetController(IQuestionSetService questionSetService)
		{
			_questionSetService = questionSetService;
		}

		[HttpGet]
		public async Task<ActionResult<QuestionSet[]>> Get()
		{
			return Ok(await _questionSetService.GetCollection());
		}
	}
}
