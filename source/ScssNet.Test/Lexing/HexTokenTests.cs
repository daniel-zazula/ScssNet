using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class HexTokenTests
	{
		[DataTestMethod]
		[DataRow("#123")]
		[DataRow("#def")]
		[DataRow("#dEf")]
		[DataRow("#DEF")]
		[DataRow("#1b3")]
		[DataRow("#1B3")]
		[DataRow("#a2C")]
		[DataRow("#a2C")]
		[DataRow("#A2c")]
		[DataRow("#A2C")]
		[DataRow("#654321")]
		[DataRow("#fedcba")]
		[DataRow("#FeDcBa")]
		[DataRow("#FEDCBA")]
		[DataRow("#6e4c2a")]
		[DataRow("#6E4c2A")]
		[DataRow("#6E4C2A")]
		[DataRow("#f5d3b1")]
		[DataRow("#f5D3b1")]
		[DataRow("#F5D3B1")]
		public void ShouldParseHex(string value)
		{
			var sourceReader = new SourceReaderMock(value);
			var hexParser = new HexParser();

			var unitToken = hexParser.Parse(sourceReader);

			unitToken.Should().NotBeNull();
			unitToken!.Value.Should().Be(value);
			sourceReader.End.Should().BeTrue();
		}
	}
}
