namespace ScssNet.Structures;

public class RuleSet(SelectorList selectorlist, Block ruleBlock) : ISyntaxStructure, INestableStatement
{
	public SelectorList SelectorList => selectorlist;
	public Block RuleBlock => ruleBlock;

	public SourceSpan Span => SourceSpan.From(selectorlist, ruleBlock);

	public Issues Issues => Issues.ConcatFrom(SelectorList, RuleBlock);
}
