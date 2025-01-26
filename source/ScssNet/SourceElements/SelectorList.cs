namespace ScssNet.SourceElements;

public class SelectorList(ICollection<ISelector> selectors) : ISourceElement
{
	public ICollection<ISelector> Selectors => selectors;

	public IEnumerable<Issue> Issues => selectors.ConcatIssues();

	public SourceCoordinates Start => Selectors.First().Start;

	public SourceCoordinates End => Selectors.Last().End;
}
