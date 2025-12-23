using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class UnitValueParserTests
{
	internal static IEnumerable<object[]> UnitValueParams = new[]
	{
		"3", "10px", "50%", "-10.2cm", "-200mm", "2Q", "-3.5in", "10pt", "5pc"
	}.ToParams();

	[DataTestMethod]
	[DataRow("3", "")]
	[DataRow("10", "px")]
	[DataRow("50", "%")]
	[DataRow("-10.2", "cm")]
	[DataRow("-200", "mm")]
	[DataRow("2", "Q")]
	[DataRow("-3.5", "in")]
	[DataRow("10", "pt")]
	[DataRow("5", "pc")]
	public void ShouldParseUnit(string amount, string unit)
	{
		var sourceReader = new SourceReaderMock($"{amount}{unit}");
		var unitParser = new UnitValueParser();

		var unitToken = unitParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		unitToken.ShouldNotBeNull();
		unitToken!.Amount.ShouldBe(decimal.Parse(amount));
		unitToken!.Unit.ShouldBe(unit);
		unitToken.LeadingSeparator.ShouldBe(Separator.Empty);
		unitToken.TrailingSeparator.ShouldBe(Separator.Empty);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonUnitValues => CommentParserTests.CommentParams
		.Concat(HexValueParserTests.HexValueParams)
		.Concat(IdentifierParserTests.IdentifierParams)
		.Concat(StringParserTests.StringParams)
		.Concat(SymbolParserTests.SymbolParams);

	[DataTestMethod]
	[DynamicData(nameof(NonUnitValues))]
	public void ShouldNotParseNonUnitValues(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var unitValueParser = new UnitValueParser();

		var unitValue = unitValueParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		unitValue.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}
}
