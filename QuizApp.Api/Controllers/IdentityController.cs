using Microsoft.AspNetCore.Mvc;
using QuizApp.Application.Dtos;
using QuizApp.Application.Services;
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
		public async Task<IActionResult> SignUp([FromBody] SignUpDto signUpDto)
		{
			await _identityService.SignUpAsync(signUpDto);
			return Ok();
		}
	}
}
