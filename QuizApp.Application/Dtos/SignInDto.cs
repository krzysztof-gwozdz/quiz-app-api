namespace QuizApp.Application.Dtos
{
	public class SignInDto
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public SignInDto()
		{
		}

		public SignInDto(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
