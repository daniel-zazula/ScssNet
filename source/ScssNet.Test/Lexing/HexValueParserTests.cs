using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class HexValueParserTests
{
	private static readonly IEnumerable<object[]> HexValues = new[]
	{
		"#123", "#def", "#dEf", "#DEF", "#1b3", "#1B3", "#a2C", "#a2C", "#A2c", "#A2C",
		"#654321", "#fedcba", "#FeDcBa", "#FEDCBA", "#6e4c2a", "#6E4c2A", "#6E4C2A", "#f5d3b1", "#f5D3b1", "#F5D3B1"
	}.ToParams();
	public static IEnumerable<object[]> HexValueParams => HexValues;
	private static readonly Separator? LeadingSeparator = null;
	private static readonly Separator TrailingSeparator = new([]);

	[DataTestMethod]
	[DynamicData(nameof(HexValueParams))]
	public void ShouldParseHexValues(string value)
	{
		var sourceReader = new SourceReaderMock(value);
		var hexParser = new HexValueParser();

		var unitToken = hexParser.Parse(sourceReader, LeadingSeparator, () => TrailingSeparator);

		unitToken.ShouldNotBeNull();
		unitToken!.Value.ShouldBe(value);
		unitToken.LeadingSeparator.ShouldBe(LeadingSeparator);
		unitToken.TrailingSeparator.ShouldBe(TrailingSeparator);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonHexValues => CommentParserTests.CommentParams
		.Concat(IdentifierParserTests.IdentifierParams)
		.Concat(StringParserTests.StringParams)
		.Concat(SymbolParserTests.SymbolParams)
		.Concat(UnitValueParserTests.UnitValueParams);

	[DataTestMethod]
	[DynamicData(nameof(NonHexValues))]
	public void ShouldNotParseNonHexValues(string value)
	{
		var sourceReader = new SourceReaderMock(value);
		var hexValueParser = new HexValueParser();

		var hexValue = hexValueParser.Parse(sourceReader, LeadingSeparator, () => TrailingSeparator);

		hexValue.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}
}
