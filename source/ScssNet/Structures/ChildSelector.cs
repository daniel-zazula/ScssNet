using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ChildSelector
(
	ISelector parentSelector, SymbolToken childOperatorSymbolToken, ISelector selector
) : SourceElement, ISyntaxStructure, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector ParentSelector => parentSelector;

	public SourceSpan Span => SourceSpan.From(parentSelector, selector);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(parentSelector, childOperatorSymbolToken, selector);
}
