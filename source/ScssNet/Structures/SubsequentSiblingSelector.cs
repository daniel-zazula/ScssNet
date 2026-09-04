using ScssNet.Tokens;

namespace ScssNet.Structures;

public class SubsequentSiblingSelector
(
	ISelector precedingSiblingSelector, SymbolToken subsequentSiblingSymbolToken, ISelector selector
) : SourceElement, ISyntaxStructure, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector PrecedingSiblingSelector => precedingSiblingSelector;

	public SourceSpan Span => SourceSpan.From(precedingSiblingSelector, selector);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(precedingSiblingSelector, subsequentSiblingSymbolToken, selector);
}
