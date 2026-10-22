using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IRuleParser: IParser<Rule> { }

internal class RuleParser(ParserContext parserContext): IRuleParser
{
	private IValueParser ValueParser => parserContext.GetParser<IValueParser>();

	public Rule? Parse(TokenReader tokenReader)
	{
		var property = tokenReader.MatchIdentifier();
		if(property is null)
			return null;

		var colon = tokenReader.RequireSymbol(Symbol.Colon);
		var value = ValueParser.RequireCommaOrSpacedList(tokenReader);
		var important = ParseImportant(tokenReader);
		var semiColon = tokenReader.MatchSymbol(Symbol.SemiColon);

		return new Rule(property, colon, value, important, semiColon);
	}

	private ImportantValue? ParseImportant(TokenReader tokenReader)
	{
		var exclamation = tokenReader.MatchSymbol(Symbol.Exclamation);
		if (exclamation is null)
			return null;

		var importantKeyword = tokenReader.RequireKeyword<ValueKeyword>();
		return new ImportantValue(exclamation, importantKeyword);
	}
}
