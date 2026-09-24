using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class TokenReaderIdentifierTests : TokenReaderTestBase
{
	public static IEnumerable<object[]> IdentifierParams => AddSpacing(TokensTestData.Identifiers);

	[TestMethod]
	[DynamicData(nameof(IdentifierParams))]
	public void ShouldMatchIdentifier(string source, string expected)
	{
		var tokenReader = SetupTokenReader(source);

		var identifierToken = tokenReader.MatchIdentifier().ShouldNotBeNull();

		AssertSeparators(source, identifierToken.LeadingSeparator, identifierToken.TrailingSeparator);

		tokenReader.End.ShouldBeTrue();
		identifierToken.Text.ShouldBe(expected);
		identifierToken.Issues.ShouldBeEmpty();
	}

	[TestMethod]
	[DynamicData(nameof(IdentifierParams))]
	public void ShouldRequireIdentifierWithoutIssues(string source, string expected)
	{
		var tokenReader = SetupTokenReader(source);

		var identifierToken = tokenReader.RequireIdentifier();

		identifierToken.Text.ShouldBe(expected);
		identifierToken.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonIdentifierParams => TokensTestData.OneOfEach
		.Except(TokensTestData.Identifiers).ToParams();

	[TestMethod]
	[DynamicData(nameof(NonIdentifierParams))]
	public void ShouldRequireNonIdentifierWithIssue(string source)
	{
		var tokenReader = SetupTokenReader(source);

		var identifierToken = tokenReader.RequireIdentifier();
		identifierToken.Text.ShouldBe("");
		AssertExpectedTokenIssue(identifierToken);

		tokenReader.End.ShouldBeFalse();
	}
}
