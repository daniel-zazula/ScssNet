using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ChildSelector
(
	ISelector parentSelector, SymbolToken childOperatorSymbolToken, ISelector selector
) : IComplexSelector
{
	public ISelector Selector => selector;

	public IEnumerable<Issue> Issues => SourceElement.List(parentSelector, childOperatorSymbolToken, selector).ConcatIssues();

	public SourceCoordinates Start => parentSelector.Start;

	public SourceCoordinates End => selector.End;
}
