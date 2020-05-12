using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;

namespace QuizApp.Api.Controllers
{
	[ApiController]
	[Route("question-sets")]
	public class QuestionSetsController : ControllerBase
	{
		private IQuestionSetsService _questionSetService;

		public QuestionSetsController(IQuestionSetsService questionSetService)
		{
			_questionSetService = questionSetService;
		}

		[HttpGet("")]
		public async Task<ActionResult<QuestionSetsDto>> Get()
		{
			return Ok(await _questionSetService.GetCollectionAsync());
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<QuestionSetDto>> Get(Guid id)
		{
			return Ok(await _questionSetService.GetAsync(id));
		}

		[HttpPost("")]
		public async Task<ActionResult> Create(CreateQuestionSetDto createQuestionSetDto)
		{
			return Created((await _questionSetService.CreateAsync(createQuestionSetDto)).ToString(), null);
		}
	}
}
