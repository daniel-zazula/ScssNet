using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class IdentifierParserTests
{
	public static IEnumerable<object[]> IdentifierParams => TokensTestData.Identifiers.ToParams();

	[TestMethod]
	[DynamicData(nameof(IdentifierParams))]
	public void ShouldParseIdentifier(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var identifierParser = new IdentifierParser();

		var identifierToken = identifierParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		identifierToken.ShouldNotBeNull();
		identifierToken!.Text.ShouldBe(source);
		identifierToken.LeadingSeparator.ShouldBe(Separator.Empty);
		identifierToken.TrailingSeparator.ShouldBe(Separator.Empty);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonIdentifiers => TokensTestData.AllTokens
		.Except(TokensTestData.Identifiers).ToParams();

	[TestMethod]
	[DynamicData(nameof(NonIdentifiers))]
	public void ShouldNotParseNonIdentifiers(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var identifierParser = new IdentifierParser();

		var identifier = identifierParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		identifier.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}

}
