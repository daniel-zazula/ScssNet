using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class StringParserTests
{
	internal static readonly string[] Strings = [ "\"Some string\"", "'Other string'" ];
	public static IEnumerable<object[]> StringParams => Strings.ToParams();

	[TestMethod]
	[DynamicData(nameof(StringParams))]
	public void ShouldParseString(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var stringParser = new StringParser();

		var stringToken = stringParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		stringToken.ShouldNotBeNull();
		stringToken!.Text.ShouldBe(source);
		stringToken.LeadingSeparator.ShouldBe(Separator.Empty);
		stringToken.TrailingSeparator.ShouldBe(Separator.Empty);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonStrings => CommentParserTests.Comments
		.Concat(HashValueParserTests.HashValues)
		.Concat(IdentifierParserTests.Identifiers)
		.Concat(SymbolParserTests.SymbolStrings)
		.Concat(UnitValueParserTests.UnitValues)
		.ToParams();

	[TestMethod]
	[DynamicData(nameof(NonStrings))]
	public void ShouldNotParseNonStrings(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var stringParser = new StringParser();

		var @string = stringParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		@string.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}
}
