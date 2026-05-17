namespace ScssNet.Structures;

public class RuleSet(SelectorList selectorlist, Block ruleBlock) : SourceElement, ISyntaxStructure
{
	public SelectorList SelectorList => selectorlist;
	public Block RuleBlock => ruleBlock;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(SelectorList, RuleBlock);

	public SourceCoordinates Start => SelectorList.Start;

	public SourceCoordinates End => RuleBlock.End;
}
