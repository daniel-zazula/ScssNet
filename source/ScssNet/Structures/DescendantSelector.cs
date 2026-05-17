namespace ScssNet.Structures;

public class DescendantSelector
(
	ISelector ascendantSelector, ISelector selector
) : IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector AscendantSelector => ascendantSelector;

	public IEnumerable<Issue> Issues => SourceElement.List(ascendantSelector, selector).ConcatIssues();

	public SourceCoordinates Start => ascendantSelector.Start;

	public SourceCoordinates End => selector.End;
}
