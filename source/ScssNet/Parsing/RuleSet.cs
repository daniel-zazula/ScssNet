using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class RuleSet(ISelector selector, Block ruleBlock)
	{
		public ISelector Selector => selector;
		public Block RuleBlock => ruleBlock;
	}

	internal class RuleSetParser(Lazy<SelectorParser> selectorParser, Lazy<BlockParser> blockParser) : ParserBase
	{
		internal RuleSet? Parse(TokenReader tokenReader)
		{
			var selector = selectorParser.Value.Parse(tokenReader);
			if(selector == null)
				return null;

			var ruleBlock = blockParser.Value.Parse(tokenReader) ?? throw new NotImplementedException("Handle missing block");
			return new RuleSet(selector, ruleBlock);
		}
	}
}
