using QuizApp.Shared.Exceptions;

namespace QuizApp.Core.Exceptions
{
	public class TagNotFoundException : NotFoundException
	{
		public override string Code => "tag_not_found";

		public TagNotFoundException(string tagName) : base($"Tag: {tagName} not found.")
		{
		}
	}
}
