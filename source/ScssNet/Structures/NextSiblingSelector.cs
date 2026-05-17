using ScssNet.Tokens;

namespace ScssNet.Structures;

public class NextSiblingSelector
(
	ISelector previousSiblingSelector, SymbolToken nextSiblingSymbolToken, ISelector selector
) : IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector PreviousSiblingSelector => previousSiblingSelector;

	public IEnumerable<Issue> Issues => SourceElement.List(previousSiblingSelector, nextSiblingSymbolToken, selector).ConcatIssues();

	public SourceCoordinates Start => previousSiblingSelector.Start;

	public SourceCoordinates End => selector.End;
}
