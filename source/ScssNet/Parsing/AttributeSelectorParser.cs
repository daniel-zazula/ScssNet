using ScssNet.Structures;
using ScssNet.Lexing;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface IAttributeSelectorParser: IParser<AttributeSelector> { }

internal class AttributeSelectorParser(ParserContext parserContext): IAttributeSelectorParser
{
	private ISelectorQualifierParser SelectorQualifierParser => parserContext.GetParser<ISelectorQualifierParser>();

	public AttributeSelector? Parse(TokenReader tokenReader)
	{
		var openBracket = tokenReader.MatchSymbol(Symbol.OpenBracket);
		if(openBracket is null)
			return null;

		var attribute = tokenReader.RequireIdentifier();

		Symbol[] operators = [Symbol.Equals, Symbol.ContainsWord, Symbol.StartsWithWord, Symbol.StartsWith, Symbol.EndsWith, Symbol.Contains];
		var @operator = tokenReader.MatchSymbol(operators);
		StringToken? value = null;
		IdentifierToken? modifier = null;
		if(@operator != null)
		{
			value = tokenReader.RequireString();
			modifier = tokenReader.MatchIdentifier();
		}

		var closeBracket = tokenReader.RequireSymbol(Symbol.CloseBracket);

		var selectorQualifier = closeBracket.TrailingSeparator == Separator.Empty
			? SelectorQualifierParser.Parse(tokenReader)
			: default;

		return new AttributeSelector(openBracket, attribute, @operator, value, modifier, closeBracket, selectorQualifier);
	}
}
