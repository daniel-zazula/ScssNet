using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class StringParserTests
{
	private static readonly string[] strings = [ "\"Some string\"", "'Other string'" ];
	public static IEnumerable<object[]> StringParams => strings.ToParams();
	private static readonly Separator? LeadingSeparator = null;
	private static readonly Separator TrailingSeparator = new([]);

	[DataTestMethod]
	[DynamicData(nameof(StringParams))]
	public void ShouldParseString(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var stringParser = new StringParser();

		var stringToken = stringParser.Parse(sourceReader, LeadingSeparator, () => TrailingSeparator);

		stringToken.ShouldNotBeNull();
		stringToken!.Text.ShouldBe(source);
		stringToken.LeadingSeparator.ShouldBe(LeadingSeparator);
		stringToken.TrailingSeparator.ShouldBe(TrailingSeparator);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonStrings => CommentParserTests.CommentParams
		.Concat(HexValueParserTests.HexValueParams)
		.Concat(IdentifierParserTests.IdentifierParams)
		.Concat(SymbolParserTests.SymbolParams)
		.Concat(UnitValueParserTests.UnitValueParams);

	[DataTestMethod]
	[DynamicData(nameof(NonStrings))]
	public void ShouldNotParseNonStrings(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var stringParser = new StringParser();

		var @string = stringParser.Parse(sourceReader, LeadingSeparator, () => TrailingSeparator);

		@string.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}
}
