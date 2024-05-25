using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class IdentifierParserTests
	{
		[DataTestMethod]
		[DataRow("table")]
		[DataRow("customClass")]
		public void ShouldParseIdentifier(string source)
		{
			var sourceReader = new SourceReaderMock(source);
			var identifierParser = new IdentifierParser();

			var identifierToken = identifierParser.Parse(sourceReader);

			identifierToken.Should().NotBeNull();
			identifierToken!.Text.Should().Be(source);
			sourceReader.End.Should().BeTrue();
		}
	}
}
