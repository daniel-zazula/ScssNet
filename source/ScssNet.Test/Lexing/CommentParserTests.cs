using ScssNet.Lexing;
using Shouldly;

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

			comment.ShouldBeNull();
			sourceReader.End.ShouldBeFalse();
		}

		private static void TestCommentParsing(string source, string commentText, string remainingSource = "")
		{
			var sourceReader = new SourceReaderMock(source);
			var commentParser = new CommentParser();

			var comment = commentParser.Parse(sourceReader);

			comment.ShouldNotBeNull();
			comment!.Text.ShouldBe(commentText);

			var remainingSourceLength = remainingSource.Length;
			if (remainingSourceLength > 0)
				sourceReader.Peek(remainingSourceLength).ShouldBe(remainingSource);
			else
				sourceReader.End.ShouldBeTrue();
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
