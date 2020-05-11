using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Api.Dtos;
using QuizApp.Api.Services;

namespace QuizApp.Api.Controllers
{
	[ApiController]
	[Route("question-sets")]
	public class QuestionSetsController : ControllerBase
	{
		private IQuestionSetService _questionSetService;

		public QuestionSetsController(IQuestionSetService questionSetService)
		{
			_questionSetService = questionSetService;
		}

		[HttpGet("")]
		public async Task<ActionResult<QuestionSets>> Get()
		{
			return Ok(await _questionSetService.GetCollection());
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<QuestionSet>> Get(Guid id)
		{
			return Ok(await _questionSetService.Get(id));
		}
	}
}
