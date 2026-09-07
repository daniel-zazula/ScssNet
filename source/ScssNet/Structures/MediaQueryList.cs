using ScssNet.Tokens;

namespace ScssNet.Structures;

public class MediaQueryList : SourceElement, ISyntaxStructure, IMediaQuery
{
	public ICollection<MediaQueryListItem> Items { get; }

	public SourceSpan Span => SourceSpan.From(Items);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Items.Cast<ISourceElement>());

	public MediaQueryList(ICollection<MediaQueryListItem> items)
	{
		if(items.Count == 0)
			throw new ArgumentException("Items cannot be empty", nameof(items));

		Items = Array.AsReadOnly(items.ToArray());
	}
}

public class MediaQueryListItem : ListItem, ISyntaxStructure, ISourceElement
{
	public IMediaQueryExpression Value { get; }

	protected override ISourceElement Item => Value;

	public MediaQueryListItem(IMediaQueryExpression value, SymbolToken? comma = null)
	{
		Value = value;
		Comma = comma;
	}
}
