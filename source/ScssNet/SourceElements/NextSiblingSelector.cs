using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class NextSiblingSelector
(
	ISelector previousSiblingSelector, SymbolToken subsequentSiblingSymbolToken, ISelector selector
) : IComplexSelector
{
	public ISelector Selector => selector;

	public IEnumerable<Issue> Issues => SourceElement.List(previousSiblingSelector, subsequentSiblingSymbolToken, selector).ConcatIssues();

	public SourceCoordinates Start => previousSiblingSelector.Start;

	public SourceCoordinates End => selector.End;
}
