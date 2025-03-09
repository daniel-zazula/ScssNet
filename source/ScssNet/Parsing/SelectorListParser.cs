using ScssNet.Lexing;
using ScssNet.SourceElements;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class SelectorListParser(Lazy<SelectorParser> selectorParser)
{
	internal SelectorList? Parse(TokenReader tokenReader)
	{
		var selector = ParseSelector(selectorParser, tokenReader);
		if(selector == null)
			return null;

		var selectors = new List<ISelector>
		{
			selector
		};

		SymbolToken? commaToken;
		do
		{
			selector = ParseSelector(selectorParser, tokenReader);
			if (selector == null)
				break;

			selectors.Add(selector);
			commaToken = tokenReader.Match(Symbol.Comma);
		}
		while(commaToken != null);

		return new SelectorList([.. selectors]);

		static ISelector? ParseSelector(Lazy<SelectorParser> selectorParser, TokenReader tokenReader)
			=> selectorParser.Value.Parse(tokenReader);
	}
}
