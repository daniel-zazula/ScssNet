using ScssNet.Tokens;

namespace ScssNet.Structures;

public class SubsequentSiblingSelector
(
	ISelector precedingSiblingSelector, SymbolToken subsequentSiblingSymbolToken, ISelector selector
) : IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector PrecedingSiblingSelector => precedingSiblingSelector;

	public IEnumerable<Issue> Issues => SourceElement.List(precedingSiblingSelector, subsequentSiblingSymbolToken, selector).ConcatIssues();

	public SourceCoordinates Start => precedingSiblingSelector.Start;

	public SourceCoordinates End => selector.End;
}
