using ScssNet.Lexing;
using ScssNet.Tokens;
using Shouldly;

namespace ScssNet.Test.Lexing;

[TestClass]
public class SymbolParserTests
{
	internal static IEnumerable<object[]> SymbolParams = new[]
	{
		".", ":", ";", "{", "}", "[", "]", "=", "~=", "|=", "^=", "$=", "*="
	}.ToParams();
	private static readonly Separator? LeadingSeparator = null;
	private static readonly Separator TrailingSeparator = new([]);

	[DataTestMethod]
	[DataRow(".", Symbol.Dot)]
	[DataRow("#", Symbol.Hash)]
	[DataRow(":", Symbol.Colon)]
	[DataRow(";", Symbol.SemiColon)]
	[DataRow("{", Symbol.OpenBrace)]
	[DataRow("}", Symbol.CloseBrace)]
	[DataRow("[", Symbol.OpenBracket)]
	[DataRow("]", Symbol.CloseBracket)]
	[DataRow("=", Symbol.Equals)]
	[DataRow("~=", Symbol.ContainsWord)]
	[DataRow("|=", Symbol.StartsWithWord)]
	[DataRow("^=", Symbol.StartsWith)]
	[DataRow("$=", Symbol.EndsWith)]
	[DataRow("*=", Symbol.Contains)]
	public void ShouldParseString(string source, Symbol symbol)
	{
		var sourceReader = new SourceReaderMock(source);
		var symbolParser = new SymbolParser();

		var symbolToken = symbolParser.Parse(sourceReader, LeadingSeparator, () => TrailingSeparator);

		symbolToken.ShouldNotBeNull();
		symbolToken!.Symbol.ShouldBe(symbol);
		symbolToken.LeadingSeparator.ShouldBe(LeadingSeparator);
		symbolToken.TrailingSeparator.ShouldBe(TrailingSeparator);
		sourceReader.End.ShouldBeTrue();
	}

	public static IEnumerable<object[]> NonSymbols => CommentParserTests.CommentParams
		.Concat(IdentifierParserTests.IdentifierParams)
		.Concat(StringParserTests.StringParams)
		.Concat(UnitValueParserTests.UnitValueParams);

	[DataTestMethod]
	[DynamicData(nameof(NonSymbols))]
	public void ShouldNotParseNonSymbols(string source)
	{
		var sourceReader = new SourceReaderMock(source);
		var symbolParser = new SymbolParser();

		var symbol = symbolParser.Parse(sourceReader, LeadingSeparator, () => TrailingSeparator);

		symbol.ShouldBeNull();
		sourceReader.End.ShouldBeFalse();
	}
}
