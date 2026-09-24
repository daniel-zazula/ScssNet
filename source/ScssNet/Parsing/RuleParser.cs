using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class RuleParser(Lazy<ValueParser> valueParser)
{
	internal Rule? Parse(TokenReader tokenReader)
	{
		var property = tokenReader.MatchIdentifier();
		if(property is null)
			return null;

		var colon = tokenReader.RequireSymbol(Symbol.Colon);
		var value = valueParser.Value.Parse(tokenReader) ?? throw new NotImplementedException("Handle missing value");
		var important = ParseImportant(tokenReader);
		var semiColon = tokenReader.MatchSymbol(Symbol.SemiColon);

		return new Rule(property, colon, value, important, semiColon);
	}

	private ImportantValue? ParseImportant(TokenReader tokenReader)
	{
		var exclamation = tokenReader.MatchSymbol(Symbol.Exclamation);
		if (exclamation is null)
			return null;

		var importantKeyword = tokenReader.RequireKeyword<ValueKeywordToken>();
		return new ImportantValue(exclamation, importantKeyword);
	}
}
