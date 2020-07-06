using FluentAssertions;
using QuizApp.Core.Exceptions;
using QuizApp.Core.Models;
using QuizApp.Core.Tests.Examples;
using QuizApp.Shared.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace QuizApp.Core.Tests.Models
{
	public class QuizTests
	{
		[Fact]
		public void CreateQuizWith4Questions_TotalQuestionsIsEqual4ButCorrectAnswers0()
		{
			//arrange

			//act
			var quiz = QuizExample.GetValidQuiz(4, 2);

			//assert
			quiz.TotalQuestions.Should().Be(4);
			quiz.CorrectAnswers.Should().Be(0);
		}

		[Fact]
		public void AnswerCorrectlyToAllQuestionInQuiz_TotalQuestionIsEqualCorrectAnswers()
		{
			//arrange
			var quiz = QuizExample.GetValidQuiz(4, 4);
			var rating = QuizExample.PlayerAnswer.ValidRating;
			var playerAnswers = quiz.Questions.Select(question => Quiz.PlayerAnswer.Create(question.Id, question.CorrectAnswerId, (int?)rating)).ToHashSet();

			//act
			quiz.Solve(playerAnswers);

			//assert
			quiz.TotalQuestions.Should().Be(quiz.CorrectAnswers);
		}

		[Fact]
		public void AnswerIncorrectlyToAllQuestionInQuiz_CorrectAnswersEqual0()
		{
			//arrange
			var quiz = QuizExample.GetValidQuiz(4, 4);
			var rating = QuizExample.PlayerAnswer.ValidRating;
			var playerAnswers = quiz.Questions
				.Select(question => Quiz.PlayerAnswer.Create(question.Id, question.Answers.First(answer => answer.Id != question.CorrectAnswerId).Id, (int?)rating))
				.ToHashSet();

			//act
			quiz.Solve(playerAnswers);

			//assert
			quiz.CorrectAnswers.Should().Be(0);
		}

		[Fact]
		public void AnswerToQuestionNotFromQuiz_ThrownException()
		{
			//arrange
			var quiz = QuizExample.GetValidQuiz(4, 4);
			var incorrectQuestionId = QuizExample.Question.NewId;
			var rating = QuizExample.PlayerAnswer.ValidRating;
			var playerAnswers = new HashSet<Quiz.PlayerAnswer>
			{
				Quiz.PlayerAnswer.Create(incorrectQuestionId, quiz.Questions.First().CorrectAnswerId, (int?)rating)
			};

			//act
			Action solveQuiz = () => quiz.Solve(playerAnswers);

			//assert
			solveQuiz.Should().Throw<QuestionIsNotAPartOfQuizException>()
				.WithMessage($"Question: {incorrectQuestionId} is not a part of quiz: {quiz.Id}.");
		}

		[Fact]
		public void UseAnswerWhichIsNotAPartOfQuestion_ThrownException()
		{
			//arrange
			var quiz = QuizExample.GetValidQuiz(4, 4);
			var questionId = quiz.Questions.First().Id;
			var incorrectAnswerId = QuizExample.Question.Answer.NewId;
			var rating = QuizExample.PlayerAnswer.ValidRating;
			var playerAnswers = new HashSet<Quiz.PlayerAnswer> { Quiz.PlayerAnswer.Create(questionId, incorrectAnswerId, (int?)rating) };

			//act
			Action solveQuiz = () => quiz.Solve(playerAnswers);

			//assert
			solveQuiz.Should().Throw<AnswerIsNotFromQuestionException>()
				.WithMessage($"Answer: {incorrectAnswerId} is not from question: {questionId}.");
		}

		public class Question
		{
			[Fact]
			public void CreateQuestion_IsNotAnswered()
			{
				//arrange

				//act
				var question = QuizExample.Question.GetValidQuestion(4);

				//assert
				question.IsAnswered.Should().BeFalse();
			}

			[Fact]
			public void AnswerQuestion_IsAnswered()
			{
				//arrange
				var question = QuizExample.Question.GetValidQuestion(4);

				//act
				question.AnswerQuestion(question.Answers.First().Id);

				//assert
				question.IsAnswered.Should().BeTrue();
			}

			[Fact]
			public void CreateQuestion_IsCorrectNotSet()
			{
				//arrange

				//act
				var question = QuizExample.Question.GetValidQuestion(4);

				//assert
				question.IsCorrect.Should().BeNull();
			}

			[Fact]
			public void AnswerQuestionCorrectly_IsCorrect()
			{
				//arrange
				var question = QuizExample.Question.GetValidQuestion(4);

				//act
				question.AnswerQuestion(question.CorrectAnswerId);

				//assert
				question.IsCorrect.Should().BeTrue();
			}

			[Fact]
			public void AnswerQuestionIncorrectly_IsNotCorrect()
			{
				//arrange
				var question = QuizExample.Question.GetValidQuestion(4);

				//act
				question.AnswerQuestion(question.Answers.First(x => x.Id != question.CorrectAnswerId).Id);

				//assert
				question.IsCorrect.Should().BeFalse();
			}
		}

		public class PlayerAnswer
		{
			[Fact]
			public void CreatePlayerAnswerWithCorrectValues_PlayerAnswerCreated()
			{
				//arrange
				var questionId = QuizExample.PlayerAnswer.NewQuestionId;
				var answerId = QuizExample.PlayerAnswer.NewAnswerId;
				var rating = QuizExample.PlayerAnswer.ValidRating;

				//act
				var playerAnswer = Quiz.PlayerAnswer.Create(questionId, answerId, (int?)rating);

				//assert
				playerAnswer.QuestionId.Should().Be(questionId);
				playerAnswer.AnswerId.Should().Be(answerId);
				playerAnswer.Rating.Should().Be(rating);
			}

			[Theory]
			[InlineData(-2)]
			[InlineData(2)]
			public void CreatePlayerAnswerWithRatingOutOfRange_ThrownException(int rating)
			{
				//arrange
				var questionId = QuizExample.PlayerAnswer.NewQuestionId;
				var answerId = QuizExample.PlayerAnswer.NewAnswerId;

				//act
				Action createPlayerAnswer = () => Quiz.PlayerAnswer.Create(questionId, answerId, rating);

				//assert
				createPlayerAnswer.Should().Throw<ValidationException>()
					.WithMessage($"rating: Question rating: {rating} is out of range.");
			}
		}
	}
}
