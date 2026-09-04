using ScssNet.Tokens;

namespace ScssNet.Structures;

public class SelectorList : SourceElement, ISyntaxStructure
{
	public ICollection<SelectorListItem> Items { get; }

	public SourceSpan Span => SourceSpan.From(Items);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Items.Cast<ISourceElement>());

	public SelectorList(ICollection<SelectorListItem> items)
	{
		if(items.Count == 0)
			throw new ArgumentException("Items cannot be empty", nameof(items));

		Items = Array.AsReadOnly(items.ToArray());
	}
}

public class SelectorListItem : ListItem, ISyntaxStructure, ISourceElement
{
	public ISelector Selector { get; }

	protected override ISourceElement Item => Selector;

	public SelectorListItem(ISelector selector, SymbolToken? comma = null)
	{
		Selector = selector;
		Comma = comma;
	}
}
