using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ChildSelector
(
	ISelector parentSelector, SymbolToken childOperatorSymbolToken, ISelector selector
) : SourceElement, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector ParentSelector => parentSelector;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(parentSelector, childOperatorSymbolToken, selector);

	public SourceCoordinates Start => parentSelector.Start;

	public SourceCoordinates End => selector.End;
}
