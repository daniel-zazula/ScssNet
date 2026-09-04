using ScssNet.Tokens;

namespace ScssNet.Structures;

public class NextSiblingSelector
(
	ISelector previousSiblingSelector, SymbolToken nextSiblingSymbolToken, ISelector selector
) : SourceElement, ISyntaxStructure, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector PreviousSiblingSelector => previousSiblingSelector;

	public SourceSpan Span => SourceSpan.From(previousSiblingSelector, selector);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(previousSiblingSelector, nextSiblingSymbolToken, selector);
}
