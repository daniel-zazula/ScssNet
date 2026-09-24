using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class TokenReaderSymbolTests : TokenReaderTestBase
{
	public static IEnumerable<object[]> SymbolParams =>
	[
		[Symbol.Comma, templates[0]],
		[Symbol.Dot, templates[1]],
		[Symbol.Colon, templates[2]],
		[Symbol.SemiColon, templates[3]]
	];

	[TestMethod]
	[DynamicData(nameof(SymbolParams))]
	public void ShouldMatchSymbol(Symbol symbol, string template)
	{
		var source = string.Format(template, symbol.ToChars());
		var tokenReader = SetupTokenReader(source);

		var symbolToken = tokenReader.MatchSymbol(symbol).ShouldNotBeNull();
		symbolToken.Symbol.ShouldBe(symbol);
		symbolToken.Issues.ShouldBeEmpty();

		AssertSeparators(source, symbolToken.LeadingSeparator, symbolToken.TrailingSeparator);

		tokenReader.End.ShouldBeTrue();
	}

	[TestMethod]
	[DynamicData(nameof(SymbolParams))]
	public void ShouldRequireSymbolWithoutIssue(Symbol symbol, string template)
	{
		var source = string.Format(template, symbol.ToChars());
		var tokenReader = SetupTokenReader(source);

		var symbolToken = tokenReader.RequireSymbol(symbol);
		symbolToken.Symbol.ShouldBe(symbol);
		symbolToken.Issues.ShouldBeEmpty();

		tokenReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonSymbolParams => TokensTestData.OneOfEach
		.Except(TokensTestData.SymbolStrings).ToParams();

	[TestMethod]
	[DynamicData(nameof(NonSymbolParams))]
	public void ShouldRequireNonSymbolWithIssue(string source)
	{
		var tokenReader = SetupTokenReader(source);

		var symbolToken = tokenReader.RequireSymbol(Symbol.Dot);
		AssertExpectedTokenIssue(symbolToken);

		tokenReader.End.ShouldBeFalse();
	}
}
