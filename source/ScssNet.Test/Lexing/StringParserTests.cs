using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class StringParserTests
{
	public static IEnumerable<object[]> StringParams => TokensTestData.Strings.ToParams();

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

	public static IEnumerable<object[]> NonStrings => TokensTestData.AllTokens
		.Except(TokensTestData.Strings).ToParams();

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
