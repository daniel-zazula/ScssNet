using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class HashValueParserTests
{
	private static readonly string[] HexColors =
	[
		"#123", "#def", "#dEf", "#DEF", "#1b3", "#1B3", "#a2C", "#a2C", "#A2c", "#A2C",
		"#654321", "#fedcba", "#FeDcBa", "#FEDCBA", "#6e4c2a", "#6E4c2A", "#6E4C2A", "#f5d3b1", "#f5D3b1", "#F5D3B1"
	];

	private static readonly string[] IdSelectors =
	[
		"#four", "#with-dash", "#with_underscore", "#with123numbers", "#with-dash-and_underscore-123"
	];

	internal static IEnumerable<string> HashValues => HexColors.Concat(IdSelectors);

	public static IEnumerable<object[]> HashValueParams => HashValues.ToParams();

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

	public static IEnumerable<object[]> NonHexValueParams => CommentParserTests.Comments
		.Concat(IdentifierParserTests.Identifiers)
		.Concat(StringParserTests.Strings)
		.Concat(SymbolParserTests.SymbolStrings)
		.Concat(UnitValueParserTests.UnitValues)
		.ToParams();

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
