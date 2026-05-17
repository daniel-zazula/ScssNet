namespace ScssNet.Structures;

public class DescendantSelector
(
	ISelector ascendantSelector, ISelector selector
) : SourceElement, IComplexSelector
{
	public ISelector Selector => selector;

	public ISelector AscendantSelector => ascendantSelector;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(AscendantSelector, Selector);

	public SourceCoordinates Start => ascendantSelector.Start;

	public SourceCoordinates End => selector.End;
}
