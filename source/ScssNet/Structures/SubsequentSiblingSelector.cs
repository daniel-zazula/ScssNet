using ScssNet.Tokens;

namespace ScssNet.Structures;

public class SubsequentSiblingSelector
(
	ISelector precedingSiblingSelector, SymbolToken subsequentSiblingSymbolToken, ISelector selector
) : ISyntaxStructure, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector PrecedingSiblingSelector => precedingSiblingSelector;

	public SourceSpan Span => SourceSpan.From(precedingSiblingSelector, selector);

	public Issues Issues => Issues.ConcatFrom(precedingSiblingSelector, subsequentSiblingSymbolToken, selector);
}
