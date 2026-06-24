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

		var selectors = new List<ISelector>
		{
			selector
		};

		SymbolToken? commaToken = tokenReader.Match(Symbol.Comma);
		while(commaToken != null)
		{
			selector = ParseSelector();
			if (selector == null)
				break;

			selectors.Add(selector);
			commaToken = tokenReader.Match(Symbol.Comma);
		}

		return new SelectorList([.. selectors]);

		ISelector? ParseSelector() => selectorParser.Value.Parse(tokenReader);
	}
}
