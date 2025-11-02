using ScssNet.Lexing;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class CommentParserTests
{
	private static readonly string[] Comments = ["//line comment\r\n", "/*two line\r\ncomment*/"];
	internal static IEnumerable<object[]> CommentParams => Comments.ToParams();

	private static IEnumerable<object[]> NonComments => HexValueParserTests.HexValueParams
		.Concat(IdentifierParserTests.IdentifierParams)
		.Concat(StringParserTests.StringParams)
		.Concat(SymbolParserTests.SymbolParams)
		.Concat(UnitValueParserTests.UnitValueParams);

	private static readonly string[] Spacers = [" ", "\r\n", "\r", "\n"];
	private static IEnumerable<object[]> SpacerParams => Spacers.ToParams();


	[DataTestMethod]
	[DynamicData(nameof(SpacerParams))]
	public void ShouldParseSingleLineComment(string spacer)
	{
		var comment = "// single line comment";
		var remaining = spacer;

		if(spacer == " ")
		{
			comment += " ";
			remaining = "";
		}

		TestCommentParsing(comment, remaining);
	}

	[DataTestMethod]
	[DynamicData(nameof(SpacerParams))]
	public void ShouldParseMultiLineComment(string spacer)
	{
		var source = $"/*part 1{spacer}part 2*/";
		TestCommentParsing(source);
	}

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

	[TestMethod]
	public void ShouldParseUnterminatedMultiLineComment()
	{
		const string comment = "/* unterminated";
		TestCommentParsing(comment);
	}

	[TestMethod]
	public void ShouldParseSingleLineCommentAtEof()
	{
		const string comment = "// comment at eof";
		TestCommentParsing(comment);
	}

	[TestMethod]
	public void ShouldParseCommentFollowedImmediatelyByToken()
	{
		const string comment = "/*c*/";
		const string identifier = ".identifier";
		TestCommentParsing(comment, identifier);
	}

	private static void TestCommentParsing(string commentText, string remainingSource = "")
	{
		var sourceReader = new SourceReaderMock(commentText + remainingSource);
		var commentParser = new CommentParser();

		var comment = commentParser.Parse(sourceReader);

		comment.ShouldNotBeNull();
		comment!.Text.ShouldBe(commentText);

		var remainingSourceLength = remainingSource.Length;
		if(remainingSourceLength > 0)
			sourceReader.Peek(remainingSourceLength).ShouldBe(remainingSource);
		else
			sourceReader.End.ShouldBeTrue();
	}
}
