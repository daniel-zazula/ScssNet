namespace ScssNet.Structures;

public class DescendantSelector
(
	ISelector ascendantSelector, ISelector selector
) : SourceElement, ISyntaxStructure, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector AscendantSelector => ascendantSelector;

	public SourceSpan Span => SourceSpan.From(ascendantSelector, selector);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(AscendantSelector, Selector);
}
