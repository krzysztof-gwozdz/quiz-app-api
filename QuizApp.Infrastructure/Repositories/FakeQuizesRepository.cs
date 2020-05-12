using QuizApp.Core.Models;
using QuizApp.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace QuizApp.Application.Services
{
	public class FakeQuizesRepository : IQuizesRepository
	{
		public async Task<Quiz> GetAsync(Guid id) => new Quiz
			{
				Id = id,
				Questions = new HashSet<Question>()
					{
						new Question
						{
							Id = new Guid("27cc0b4c-f45c-4af1-a6e8-1dbbd3b30a57"),
							Text = "This is short question.",
							Answers = new HashSet<Answer>()
							{
								new Answer{Id = new Guid("c3c638d9-e0a3-4ab0-bd32-b504c9c3fc06"), Text="1"},
								new Answer{Id = new Guid("05c85833-ea56-4505-8aee-eeba4a895977"), Text="2"},
								new Answer{Id = new Guid("05c85833-ea56-4505-8aee-eeba4a895977"), Text="3"},
							},
							CorrectAnswerId = new Guid("c3c638d9-e0a3-4ab0-bd32-b504c9c3fc06")
						},
						new Question
						{
							Id = new Guid("50ce025e-fbba-4897-93cc-4cd0db3edf34"),
							Text = "This is normal question. Answer 1 is correct.",
							Answers = new HashSet<Answer>()
							{
								new Answer{Id = new Guid("00fac0bc-8a00-4c07-80d5-c041eb4f7016"), Text="Answer 1"},
								new Answer{Id = new Guid("dc19ef7e-7f81-4328-a904-6f443dfcc072"), Text="Answer 2"},
								new Answer{Id = new Guid("de892f2b-8692-4195-ba23-a85e4ec9cc9e"), Text="Answer 3"},
								new Answer{Id = new Guid("de892f2b-8692-4195-ba23-a85e4ec9cc9e"), Text="Answer 4"},
							},
							CorrectAnswerId = new Guid("00fac0bc-8a00-4c07-80d5-c041eb4f7016")
						},
						new Question
						{
							Id = new Guid("50ce025e-fbba-4897-93cc-4cd0db3edf34"),
							Text = "This is long question. Answer 1 is correct. Answer 2 is not correct. Answer 3 is not correct. Answer 4 is not correct. Answer 5 is not correct.",
							Answers = new HashSet<Answer>()
							{
								new Answer{Id = new Guid("00fac0bc-8a00-4c07-80d5-c041eb4f7016"), Text="Correct answer number 1"},
								new Answer{Id = new Guid("dc19ef7e-7f81-4328-a904-6f443dfcc072"), Text="Not correct answer number answer 2"},
								new Answer{Id = new Guid("de892f2b-8692-4195-ba23-a85e4ec9cc9e"), Text="Not correct answer number answer 3"},
								new Answer{Id = new Guid("de892f2b-8692-4195-ba23-a85e4ec9cc9e"), Text="Not correct answer number answer 4"},
								new Answer{Id = new Guid("de892f2b-8692-4195-ba23-a85e4ec9cc9e"), Text="Not correct answer number answer 5"},
							},
							CorrectAnswerId = new Guid("00fac0bc-8a00-4c07-80d5-c041eb4f7016")
						}
					}
			};
	}
}
