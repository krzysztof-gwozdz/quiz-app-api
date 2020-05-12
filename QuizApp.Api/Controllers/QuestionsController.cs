using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using System;
using System.Threading.Tasks;

namespace QuizApp.Api.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
		private IQuestionsService _questionsService;

		public QuestionsController(IQuestionsService questionsService)
		{
			_questionsService = questionsService;
		}

		[HttpGet("{id:guid}")]
		public async Task<ActionResult<QuestionDto>> Get(Guid id)
		{
			return Ok(await _questionsService.GetAsync(id));
		}

		[HttpPost("")]
		public async Task<ActionResult> Create(CreateQuestionDto createQuestionDto)
		{
			return Created((await _questionsService.CreateAsync(createQuestionDto)).ToString(), null);
		}

		[HttpDelete("{id:guid}")]
		public async Task<ActionResult> Create(Guid id)
		{
			await _questionsService.RemoveAsync(id);
			return Ok();
		}
	}
}