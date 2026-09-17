using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class SymbolParserTests
{
	public static IEnumerable<object[]> SymbolParams
		=> TokensTestData.Symbols.Select(s => new object[] { s });

	[TestMethod]
	[DynamicData(nameof(SymbolParams))]
	public void ShouldParseString(Symbol symbol)
	{
		var source = symbol.ToChars();
		var sourceReader = new SourceReaderMock(source);
		var symbolParser = new SymbolParser();

		var symbolToken = symbolParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		symbolToken.ShouldNotBeNull();
		symbolToken!.Symbol.ShouldBe(symbol);
		symbolToken.LeadingSeparator.ShouldBe(Separator.Empty);
		symbolToken.TrailingSeparator.ShouldBe(Separator.Empty);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonSymbols => TokensTestData.AllTokens
		.Except(TokensTestData.SymbolStrings).ToParams();

	[TestMethod]
	[DynamicData(nameof(NonSymbols))]
	public void ShouldNotParseNonSymbols(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var symbolParser = new SymbolParser();

		var symbol = symbolParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		symbol.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}
}
