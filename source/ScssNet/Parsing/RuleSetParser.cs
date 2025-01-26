using ScssNet.Lexing;
using ScssNet.SourceElements;

namespace ScssNet.Parsing
{
	internal class RuleSetParser(Lazy<SelectorListParser> selectorListParser, Lazy<BlockParser> blockParser)
	{
		internal RuleSet? Parse(TokenReader tokenReader)
		{
			var selectorList = selectorListParser.Value.Parse(tokenReader);
			if(selectorList == null)
				return null;

			var ruleBlock = blockParser.Value.Parse(tokenReader) ?? throw new NotImplementedException("Handle missing block");
			return new RuleSet(selectorList, ruleBlock);
		}
	}
}
