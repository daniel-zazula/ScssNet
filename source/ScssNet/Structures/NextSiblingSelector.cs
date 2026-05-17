using ScssNet.Tokens;

namespace ScssNet.Structures;

public class NextSiblingSelector
(
	ISelector previousSiblingSelector, SymbolToken nextSiblingSymbolToken, ISelector selector
) : SourceElement, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector PreviousSiblingSelector => previousSiblingSelector;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(previousSiblingSelector, nextSiblingSymbolToken, selector);

	public SourceCoordinates Start => previousSiblingSelector.Start;

	public SourceCoordinates End => selector.End;
}
