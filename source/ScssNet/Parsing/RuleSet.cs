using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class RuleSet(SelectorList selectorlist, Block ruleBlock)
	{
		public SelectorList Selectorlist => selectorlist;
		public Block RuleBlock => ruleBlock;
	}

	internal class RuleSetParser(Lazy<SelectorListParser> selectorListParser, Lazy<BlockParser> blockParser) : ParserBase
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
