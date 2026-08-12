using ScssNet.Lexing;
using ScssNet.Structures;

namespace ScssNet.Parsing;

internal class RuleSetParser(Lazy<SelectorListParser> selectorListParser, Lazy<BlockParser> blockParser)
{
	internal RuleSet? Parse(TokenReader tokenReader)
	{
		var selectorList = selectorListParser.Value.Parse(tokenReader);
		if(selectorList == null)
			return null;

		var ruleBlock = blockParser.Value.Require(tokenReader);
		return new RuleSet(selectorList, ruleBlock);
	}
}
