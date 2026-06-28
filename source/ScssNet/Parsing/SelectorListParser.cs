using ScssNet.Lexing;
using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal class SelectorListParser(Lazy<SelectorParser> selectorParser)
{
	internal SelectorList? Parse(ITokenReader tokenReader)
	{
		var selector = ParseSelector();
		if(selector == null)
			return null;

		SymbolToken? commaToken = tokenReader.Match(Symbol.Comma);

		var selectors = new List<SelectorListItem>
		{
			new(selector, commaToken)
		};

		while(commaToken != null)
		{
			selector = ParseSelector();
			if (selector == null)
				break;

			commaToken = tokenReader.Match(Symbol.Comma);

			selectors.Add(new(selector, commaToken));
		}

		return new SelectorList([.. selectors]);

		ISelector? ParseSelector() => selectorParser.Value.Parse(tokenReader);
	}
}
