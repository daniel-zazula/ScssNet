using ScssNet.Lexing;
using ScssNet.SourceElements;

namespace ScssNet.Parsing
{
	internal class SubSelectorParser(Lazy<SelectorParser> selectorParser)
	{
		internal SubSelector? Parse(TokenReader tokenReader, ISelector parentSelector)
		{
			var selector = selectorParser.Value.Parse(tokenReader);
			if (selector == null)
				return null;

			return new SubSelector(selector, parentSelector);
		}
	}
}
