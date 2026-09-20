using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ChildSelector
(
	ISelector parentSelector, SymbolToken childOperatorSymbolToken, ISelector selector
) : ISyntaxStructure, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector ParentSelector => parentSelector;

	public SourceSpan Span => SourceSpan.From(parentSelector, selector);

	public Issues Issues => Issues.ConcatFrom(parentSelector, childOperatorSymbolToken, selector);
}
