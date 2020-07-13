using Microsoft.AspNetCore.Mvc;
using QuizApp.Api.ErrorHandling;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
using System.Net;
using System.Threading.Tasks;

namespace QuizApp.Api.Controllers
{
	[ApiController]
	[Route("identity")]
	public class IdentityController : ControllerBase
	{
		private readonly IIdentitiesService _identityService;

		public IdentityController(IIdentitiesService identityService)
		{
			_identityService = identityService;
		}

		[HttpPost("sign-up")]
		[ProducesResponseType((int)HttpStatusCode.OK)]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
		{
			await _identityService.SignUpAsync(signUpDto);
			return Ok();
		}

		[HttpPost("sign-in")]
		[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(TokenDto))]
		[ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponse))]
		[ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponse))]
		public async Task<ActionResult<TokenDto>> SignIn([FromBody] SignInDto signInDto)
		{
			return Ok(await _identityService.SignInAsync(signInDto));
		}
	}
}
