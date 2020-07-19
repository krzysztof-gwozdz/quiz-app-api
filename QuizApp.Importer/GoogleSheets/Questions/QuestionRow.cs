namespace QuizApp.Importer.GoogleSheets.Questions
{
	internal class QuestionRow
	{
		public bool IsComplete { get; set; }
		public bool IsValid { get; set; }
		public string Tag1 { get; set; }
		public string Tag2 { get; set; }
		public string Tag3 { get; set; }
		public string Question { get; set; }
		public string Answer1 { get; set; }
		public string Answer2 { get; set; }
		public string Answer3 { get; set; }
		public string Answer4 { get; set; }


		public QuestionRow(bool isComplete, bool isValid, string tag1, string tag2, string tag3, string question, string answer1, string answer2, string answer3, string answer4)
		{
			IsComplete = isComplete;
			IsValid = isValid;
			Tag1 = tag1;
			Tag2 = tag2;
			Tag3 = tag3;
			Question = question;
			Answer1 = answer1;
			Answer2 = answer2;
			Answer3 = answer3;
			Answer4 = answer4;
		}
	}
}
