using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class HashValueParserTests
{
	private static readonly string[] HexColors = new[]
	{
		"#123", "#def", "#dEf", "#DEF", "#1b3", "#1B3", "#a2C", "#a2C", "#A2c", "#A2C",
		"#654321", "#fedcba", "#FeDcBa", "#FEDCBA", "#6e4c2a", "#6E4c2A", "#6E4C2A", "#f5d3b1", "#f5D3b1", "#F5D3B1"
	};

	private static readonly string[] IdSelectors = new[]
	{
		"#four", "#with-dash", "#with_underscore", "#with123numbers", "#with-dash-and_underscore-123"
	};

	public static IEnumerable<object[]> HashValueParams => HexColors.Concat(IdSelectors).ToParams();

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

	public static IEnumerable<object[]> NonHexValues => CommentParserTests.CommentParams
		.Concat(IdentifierParserTests.IdentifierParams)
		.Concat(StringParserTests.StringParams)
		.Concat(SymbolParserTests.SymbolParams)
		.Concat(UnitValueParserTests.UnitValueParams);

	[TestMethod]
	[DynamicData(nameof(NonHexValues))]
	public void ShouldNotParseNonHexValues(string value)
	{
		var sourceReader = new SourceReaderMock(value);
		var hexValueParser = new HashValueParser();

		var hexValue = hexValueParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		hexValue.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}
}
