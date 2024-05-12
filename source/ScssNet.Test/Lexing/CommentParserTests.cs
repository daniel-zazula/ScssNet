using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class CommentParserTests
	{
		[DataTestMethod]
		[DataRow("CR+LF")]
		[DataRow("CR")]
		[DataRow("LF")]
		public void ShouldParseSingleLineComment(string lineBreak)
		{
			const string comment = "// single line comment";
			var source = $"{comment}{ReplaceReadableLineBreak(lineBreak)}";
			TestCommentParsing(source, comment);
		}

		[DataTestMethod]
		[DataRow(" ")]
		[DataRow("CR+LF")]
		[DataRow("CR")]
		[DataRow("LF")]
		public void ShouldParseMultiLineComment(string spacer)
		{
			var source = $"/*part 1{ReplaceReadableLineBreak(spacer)}part 2*/";
			TestCommentParsing(source, source);
		}

		private static void TestCommentParsing(string source, string commentText)
		{
			var sourceReader = new SourceReaderMock(source);
			var commentParser = new CommentParser();

			var comment = commentParser.Parse(sourceReader);

			comment.Should().NotBeNull();
			comment!.Text.Should().Be(commentText);
		}

		private static string ReplaceReadableLineBreak(string possibleLineBreak)
		{
			return possibleLineBreak.ToUpper() switch
			{
				"CR+LF" => "\r\n",
				"CR" => "\r",
				"LF" => "\n",
				_ => possibleLineBreak
			};
		}
	}
}
