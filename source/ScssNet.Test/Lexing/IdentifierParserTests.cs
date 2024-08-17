using FluentAssertions;
using ScssNet.Lexing;

namespace ScssNet.Test.Lexing
{
	[TestClass]
	public class IdentifierParserTests
	{
		private static readonly string[] Identifiers = ["table", "CamelCase", "custom-class", "-experimental-property"];
		public static IEnumerable<object[]> IdentifierParams => Identifiers.ToParams();

		[DataTestMethod]
		[DynamicData(nameof(IdentifierParams))]
		public void ShouldParseIdentifier(string source)
		{
			var sourceReader = new SourceReaderMock(source);
			var identifierParser = new IdentifierParser();

			var identifierToken = identifierParser.Parse(sourceReader);

			identifierToken.Should().NotBeNull();
			identifierToken!.Text.Should().Be(source);
			sourceReader.End.Should().BeTrue();
		}

		public static IEnumerable<object[]> NonIdentifiers => CommentParserTests.CommentParams
			.Concat(HexValueParserTests.HexValueParams)
			.Concat(StringParserTests.StringParams)
			.Concat(SymbolParserTests.SymbolParams)
			.Concat(UnitValueParserTests.UnitValueParams);

		[DataTestMethod]
		[DynamicData(nameof(NonIdentifiers))]
		public void ShouldNotParseNonIdentifiers(string source)
		{
			var sourceReader = new SourceReaderMock(source);
			var identifierParser = new IdentifierParser();

			var identifier = identifierParser.Parse(sourceReader);

			identifier.Should().BeNull();
			sourceReader.End.Should().BeFalse();
		}

	}
}
