namespace ScssNet.Structures;

public class DescendantSelector
(
	ISelector ascendantSelector, ISelector selector
) : ISyntaxStructure, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector AscendantSelector => ascendantSelector;

	public SourceSpan Span => SourceSpan.From(ascendantSelector, selector);

	public Issues Issues => Issues.ConcatFrom(AscendantSelector, Selector);
}
