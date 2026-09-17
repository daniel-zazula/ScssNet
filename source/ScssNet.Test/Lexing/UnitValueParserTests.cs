using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class UnitValueParserTests
{
	public static IEnumerable<object[]> UnitValues
		=> TokensTestData.UnitValues.Select(u => new object[] { u.value, u.unit });

	[TestMethod]
	[DynamicData(nameof(UnitValues))]
	public void ShouldParseUnit(string amount, string unit)
	{
		var sourceReader = new SourceReaderMock($"{amount}{unit}");
		var unitParser = new UnitValueParser();

		var unitToken = unitParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		unitToken.ShouldNotBeNull();
		unitToken.Amount.ShouldBe(decimal.Parse(amount));
		unitToken.Unit.ShouldBe(unit);
		unitToken.LeadingSeparator.ShouldBe(Separator.Empty);
		unitToken.TrailingSeparator.ShouldBe(Separator.Empty);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonUnitValues => TokensTestData.AllTokens
		.Except(TokensTestData.UnitValueStrings).ToParams();

	[TestMethod]
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
