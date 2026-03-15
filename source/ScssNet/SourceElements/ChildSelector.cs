using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class ChildSelector
(
	ISelector parentSelector, SymbolToken childOperatorSymbolToken, ISelector selector
) : IComplexSelector
{
	public IEnumerable<Issue> Issues => SourceElement.List(parentSelector, childOperatorSymbolToken, selector).ConcatIssues();

	public SourceCoordinates Start => parentSelector.Start;

	public SourceCoordinates End => selector.End;

	public bool HasSeparatorAfter() => selector.HasSeparatorAfter();
}
