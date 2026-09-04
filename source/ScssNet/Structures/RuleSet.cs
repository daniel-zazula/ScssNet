namespace ScssNet.Structures;

public class RuleSet(SelectorList selectorlist, Block ruleBlock) : SourceElement, ISyntaxStructure, INestableStatement
{
	public SelectorList SelectorList => selectorlist;
	public Block RuleBlock => ruleBlock;

	public SourceSpan Span => SourceSpan.From(selectorlist, ruleBlock);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(SelectorList, RuleBlock);
}
