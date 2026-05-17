namespace ScssNet.Structures;

public class SelectorList(ICollection<ISelector> selectors) : SourceElement, ISyntaxStructure
{
	public ICollection<ISelector> Selectors => selectors;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Selectors.Cast<ISourceElement>());

	public SourceCoordinates Start => Selectors.First().Start;

	public SourceCoordinates End => Selectors.Last().End;
}
