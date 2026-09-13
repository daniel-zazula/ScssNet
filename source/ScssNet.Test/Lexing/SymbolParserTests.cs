using System;
using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class SymbolParserTests
{
	private static readonly Symbol[] Symbols = Enum.GetValues<Symbol>();

	internal static IEnumerable<string> SymbolStrings => Symbols.Select(s => s.ToChars());

	public static IEnumerable<object[]> SymbolParams => Symbols.Select(s => new object[] { s.ToChars(), s });

	[TestMethod]
	[DynamicData(nameof(SymbolParams))]
	public void ShouldParseString(string source, Symbol symbol)
	{
		var sourceReader = new SourceReaderMock(source);
		var symbolParser = new SymbolParser();

		var symbolToken = symbolParser.Parse(sourceReader, Separator.Empty, () => Separator.Empty);

		symbolToken.ShouldNotBeNull();
		symbolToken!.Symbol.ShouldBe(symbol);
		symbolToken.LeadingSeparator.ShouldBe(Separator.Empty);
		symbolToken.TrailingSeparator.ShouldBe(Separator.Empty);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonSymbols => CommentParserTests.Comments
		.Concat(HashValueParserTests.HashValues)
		.Concat(IdentifierParserTests.Identifiers)
		.Concat(StringParserTests.Strings)
		.Concat(UnitValueParserTests.UnitValues)
		.ToParams();

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
