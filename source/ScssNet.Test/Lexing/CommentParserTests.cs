using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class CommentParserTests
	{
		internal static IEnumerable<object[]> CommentParams = new[] { "//line comment\r\n", "/*two line\r\ncomment*/" }.ToParams();

		[DataTestMethod]
		[DataRow("CR+LF")]
		[DataRow("CR")]
		[DataRow("LF")]
		public void ShouldParseSingleLineComment(string lineBreakName)
		{
			const string comment = "// single line comment";
			string lineBreak = ReplaceReadableLineBreak(lineBreakName);
			var source = $"{comment}{lineBreak}";
			TestCommentParsing(source, comment, lineBreak);
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

		public static IEnumerable<object[]> NonComments => HexValueParserTests.HexValueParams
			.Concat(IdentifierParserTests.IdentifierParams)
			.Concat(StringParserTests.StringParams)
			.Concat(SymbolParserTests.SymbolParams)
			.Concat(UnitValueParserTests.UnitValueParams);

		[DataTestMethod]
		[DynamicData(nameof(NonComments))]
		public void ShouldNotParseNonComments(string source)
		{
			var sourceReader = new SourceReaderMock(source);
			var commentParser = new CommentParser();

			var comment = commentParser.Parse(sourceReader);

			comment.Should().BeNull();
			sourceReader.End.Should().BeFalse();
		}

		private static void TestCommentParsing(string source, string commentText, string remainingSource = "")
		{
			var sourceReader = new SourceReaderMock(source);
			var commentParser = new CommentParser();

			var comment = commentParser.Parse(sourceReader);

			comment.Should().NotBeNull();
			comment!.Text.Should().Be(commentText);

			var remainingSourceLength = remainingSource.Length;
			if (remainingSourceLength > 0)
				sourceReader.Peek(remainingSourceLength).Should().Be(remainingSource);
			else
				sourceReader.End.Should().BeTrue();
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
