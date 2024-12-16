using ScssNet.Lexing;

namespace ScssNet.Parsing
{
	public class RuleSet(SelectorList selectorlist, Block ruleBlock): ISourceElement
	{
		public SelectorList SelectorList => selectorlist;
		public Block RuleBlock => ruleBlock;

		public IEnumerable<Issue> Issues => SourceElement.List(SelectorList, RuleBlock).ConcatIssues();

		public SourceCoordinates Start => SelectorList.Start;

		public SourceCoordinates End => RuleBlock.End;
	}

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
