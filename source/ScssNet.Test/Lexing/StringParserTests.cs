using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class StringParserTests
	{
		[DataTestMethod]
		[DataRow("\"Some string\"")]
		[DataRow("'Other string'")]
		public void ShouldParseString(string source)
		{
			var sourceReader = new SourceReaderMock(source);
			var stringParser = new StringParser();

			var stringToken = stringParser.Parse(sourceReader);

			stringToken.Should().NotBeNull();
			stringToken!.Text.Should().Be(source);
			sourceReader.End.Should().BeTrue();
		}
	}
}
