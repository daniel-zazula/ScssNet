using ScssNet.Tokens;

namespace ScssNet.Structures;

public class SelectorList(ICollection<SelectorListItem> items) : SourceElement, ISyntaxStructure
{
	public ICollection<SelectorListItem> Items => items;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Items.Cast<ISourceElement>());

	public SourceCoordinates Start => Items.First().Start;

	public SourceCoordinates End => Items.Last().End;
}

public class SelectorListItem(ISelector selector, SymbolToken? comma) : SourceElement, ISyntaxStructure
{
	public ISelector Selector => selector;

	public SymbolToken? Comma => comma;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Selector, Comma);
	public SourceCoordinates Start => Selector.Start;
	public SourceCoordinates End => LastEnd(selector, comma);
}
