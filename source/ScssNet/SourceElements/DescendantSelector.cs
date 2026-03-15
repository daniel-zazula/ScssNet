namespace ScssNet.SourceElements;

public class DescendantSelector
(
	ISelector ascendantSelector, ISelector selector
) : IComplexSelector
{
	public IEnumerable<Issue> Issues => SourceElement.List(ascendantSelector, selector).ConcatIssues();

	public SourceCoordinates Start => ascendantSelector.Start;

	public SourceCoordinates End => selector.End;

	public bool HasSeparatorAfter() => selector.HasSeparatorAfter();
}
