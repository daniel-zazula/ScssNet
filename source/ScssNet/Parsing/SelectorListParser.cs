using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class SelectorListParser(Lazy<SelectorParser> selectorParser)
{
	internal SelectorList? Parse(TokenReader tokenReader)
	{
		var selector = ParseSelector();
		if(selector == null)
			return null;

		SymbolToken? commaToken = tokenReader.MatchSymbol(Symbol.Comma);

		var selectors = new List<SelectorListItem>
		{
			new(selector, commaToken)
		};

		while(commaToken != null)
		{
			selector = ParseSelector();
			if (selector == null)
				break;

			commaToken = tokenReader.MatchSymbol(Symbol.Comma);

			selectors.Add(new(selector, commaToken));
		}

		return new SelectorList([.. selectors]);

		ISelector? ParseSelector() => selectorParser.Value.Parse(tokenReader);
	}
}
