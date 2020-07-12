namespace QuizApp.Application.Dtos
{
	public class SignUpDto
	{
		public string Username { get; set; }
		public string Password { get; set; }

		public SignUpDto()
		{
		}

		public SignUpDto(string username, string password)
		{
			Username = username;
			Password = password;
		}
	}
}
