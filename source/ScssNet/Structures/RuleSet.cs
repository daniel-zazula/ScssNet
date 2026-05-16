namespace ScssNet.Structures;

public class RuleSet(SelectorList selectorlist, Block ruleBlock) : ISyntaxStructure
{
	public SelectorList SelectorList => selectorlist;
	public Block RuleBlock => ruleBlock;

	public IEnumerable<Issue> Issues => SourceElement.List(SelectorList, RuleBlock).ConcatIssues();

	public SourceCoordinates Start => SelectorList.Start;

	public SourceCoordinates End => RuleBlock.End;
}
