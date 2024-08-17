using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class UnitValueParserTests
	{
		internal static IEnumerable<object[]> UnitValueParams = new[] { "3", "10px", "50%", "-10.2cm", "-200mm", "2Q", "-3.5in", "10pt", "5pc" }.ToParams();

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

			var unitToken = unitParser.Parse(sourceReader);

			unitToken.Should().NotBeNull();
			unitToken!.Amount.Should().Be(decimal.Parse(amount));
			unitToken!.Unit.Should().Be(unit);
			sourceReader.End.Should().BeTrue();
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

			var unitValue = unitValueParser.Parse(sourceReader);

			unitValue.Should().BeNull();
			sourceReader.End.Should().BeFalse();
		}
	}
}
