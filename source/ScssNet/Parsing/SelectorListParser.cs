using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal interface ISelectorListParser: IParser<SelectorList> { }

internal class SelectorListParser(ParserContext parserContext): ISelectorListParser
{
	public SelectorList? Parse(TokenReader tokenReader)
	{
		var selectorParser = parserContext.GetParser<ISelectorParser>();

		var selector = selectorParser.Parse(tokenReader);
		if(selector == null)
			return null;

		SymbolToken? commaToken = tokenReader.MatchSymbol(Symbol.Comma);

		var selectors = new List<SelectorListItem>
		{
			new(selector, commaToken)
		};

		while(commaToken != null)
		{
			selector = selectorParser.Parse(tokenReader);
			if (selector == null)
				break;

			commaToken = tokenReader.MatchSymbol(Symbol.Comma);

			selectors.Add(new(selector, commaToken));
		}

		return new SelectorList([.. selectors]);
	}
}
