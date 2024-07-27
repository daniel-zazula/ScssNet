using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class UnitTokenTests
	{
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
			var unitParser = new UnitParser();

			var unitToken = unitParser.Parse(sourceReader);

			unitToken.Should().NotBeNull();
			unitToken!.Amount.Should().Be(decimal.Parse(amount));
			unitToken!.Unit.Should().Be(unit);
			sourceReader.End.Should().BeTrue();
		}
	}
}
