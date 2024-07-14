using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class NumberTokenTests
	{
		[DataTestMethod]
		[DataRow("3")]
		[DataRow("10")]
		[DataRow("1200")]
		[DataRow("-10.2")]
		[DataRow("-200")]
		[DataRow("-2")]
		[DataRow("-3.50")]
		public void ShouldParseUnit(string amount)
		{
			var sourceReader = new SourceReaderMock(amount);
			var numberParser = new NumberParser();

			var numberToken = numberParser.Parse(sourceReader);

			numberToken.Should().NotBeNull();
			numberToken!.Number.Should().Be(decimal.Parse(amount));
			sourceReader.End.Should().BeTrue();
		}
	}
}
