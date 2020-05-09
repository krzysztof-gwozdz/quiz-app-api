using QuizApp.Dtos;
using System;
using System.Threading.Tasks;

namespace QuizApp.Services
{
	public class FakeQuizService : IQuizService
	{
		public async Task<Quiz> Get(Guid id)
		{
			return new Quiz
			{
				Id = id,
				Questions = new[]
				{
					new Question
					{
						Id = new Guid("27cc0b4c-f45c-4af1-a6e8-1dbbd3b30a57"),
						Text = "This is short question.",
						Answers = new []
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
						Answers = new []
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
						Answers = new []
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

		public async Task<QuizSummary> GetSummary(Guid id)
		{
			return new QuizSummary
			{
				QuizId = id,
				CorrectAnswers = 1,
				TotalQuestions = 3,
				QuestionSummaries = new[]
				{
					new QuestionSummary
					{
						QuestionId = new Guid("27cc0b4c-f45c-4af1-a6e8-1dbbd3b30a57"),
						Text = "This is short question.",
						Answers = new []
						{
							new Answer{Id = new Guid("c3c638d9-e0a3-4ab0-bd32-b504c9c3fc06"), Text="1"},
							new Answer{Id = new Guid("05c85833-ea56-4505-8aee-eeba4a895977"), Text="2"},
							new Answer{Id = new Guid("d8990ead-409e-422d-a8ef-5ef63bf083be"), Text="3"},
						},
						CorrectAnswerId = new Guid("c3c638d9-e0a3-4ab0-bd32-b504c9c3fc06"),
						PlayerAnswerId = new Guid("c3c638d9-e0a3-4ab0-bd32-b504c9c3fc06"),
						IsCorrect = true,
					},
					new QuestionSummary
					{
						QuestionId = new Guid("50ce025e-fbba-4897-93cc-4cd0db3edf34"),
						Text = "This is normal question. Answer 1 is correct.",
						Answers = new []
						{
							new Answer{Id = new Guid("00fac0bc-8a00-4c07-80d5-c041eb4f7016"), Text="Answer 1"},
							new Answer{Id = new Guid("dc19ef7e-7f81-4328-a904-6f443dfcc072"), Text="Answer 2"},
							new Answer{Id = new Guid("de892f2b-8692-4195-ba23-a85e4ec9cc9e"), Text="Answer 3"},
							new Answer{Id = new Guid("562f3106-ef5d-40f2-85ca-bb040a34ab76"), Text="Answer 4"},
						},
						CorrectAnswerId = new Guid("00fac0bc-8a00-4c07-80d5-c041eb4f7016"),
						PlayerAnswerId = new Guid("562f3106-ef5d-40f2-85ca-bb040a34ab76"),
						IsCorrect = false,
					},
					new QuestionSummary
					{
						QuestionId = new Guid("50ce025e-fbba-4897-93cc-4cd0db3edf34"),
						Text = "This is long question. Answer 1 is correct. Answer 2 is not correct. Answer 3 is not correct. Answer 4 is not correct. Answer 5 is not correct.",
						Answers = new []
						{
							new Answer{Id = new Guid("00fac0bc-8a00-4c07-80d5-c041eb4f7016"), Text="Correct answer number 1"},
							new Answer{Id = new Guid("dc19ef7e-7f81-4328-a904-6f443dfcc072"), Text="Not correct answer number answer 2"},
							new Answer{Id = new Guid("de892f2b-8692-4195-ba23-a85e4ec9cc9e"), Text="Not correct answer number answer 3"},
							new Answer{Id = new Guid("4cc27efe-9ed7-4248-ba50-1ddbd8300941"), Text="Not correct answer number answer 4"},
							new Answer{Id = new Guid("569dd89a-3ee6-4cdc-b74a-bc3d298ca33b"), Text="Not correct answer number answer 5"},
						},
						CorrectAnswerId = new Guid("00fac0bc-8a00-4c07-80d5-c041eb4f7016"),
						PlayerAnswerId = new Guid("569dd89a-3ee6-4cdc-b74a-bc3d298ca33b"),
						IsCorrect = false,
					}
				}
			};
		}

		public async Task<Guid> Generate(QuizParameters quizParameters)
		{
			return new Guid("7169e682-28ed-45f1-b3bf-5221eb150486");
		}

		public async Task Solve(SolvedQuiz solvedQuiz)
		{
		}
	}
}
