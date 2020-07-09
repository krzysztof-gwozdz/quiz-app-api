using QuizApp.Shared.Exceptions;
using System;

namespace QuizApp.Core.Exceptions
{
	public class TagNotFoundException : NotFoundException
	{
		public override string Code => "tag_not_found";

		public TagNotFoundException(Guid tagId) : base($"Tag: {tagId} not found.")
		{
		}
		
		public TagNotFoundException(string tagName) : base($"Tag: {tagName} not found.")
		{
		}
	}
}
