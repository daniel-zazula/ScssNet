using ScssNet.Tokens;

namespace ScssNet.Structures;

public class SubsequentSiblingSelector
(
	ISelector precedingSiblingSelector, SymbolToken subsequentSiblingSymbolToken, ISelector selector
) : SourceElement, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector PrecedingSiblingSelector => precedingSiblingSelector;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(precedingSiblingSelector, subsequentSiblingSymbolToken, selector);

	public SourceCoordinates Start => precedingSiblingSelector.Start;

	public SourceCoordinates End => selector.End;
}
