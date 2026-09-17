using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class HashValueParserTests
{
	public static IEnumerable<object[]> HashValueParams => TokensTestData.HashValues.ToParams();

	[TestMethod]
	[DynamicData(nameof(HashValueParams))]
	public void ShouldParseHexValues(string value)
	{
		var sourceReader = new SourceReaderMock(value);
		var hexParser = new HashValueParser();

		var unitToken = hexParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		unitToken.ShouldNotBeNull();
		unitToken!.Value.ShouldBe(value);
		unitToken.LeadingSeparator.ShouldBe(Separator.Empty);
		unitToken.TrailingSeparator.ShouldBe(Separator.Empty);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonHexValueParams => TokensTestData.AllTokens
		.Except(TokensTestData.HashValues).ToParams();

	[TestMethod]
	[DynamicData(nameof(NonHexValueParams))]
	public void ShouldNotParseNonHexValues(string value)
	{
		var sourceReader = new SourceReaderMock(value);
		var hexValueParser = new HashValueParser();

		var hexValue = hexValueParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		hexValue.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}
}
