using ScssNet.Tokens;

namespace ScssNet.Structures;

public abstract class ListItem : SourceElement, ISyntaxStructure
{
	public SymbolToken? Comma { get; protected set; }

	public SourceSpan Span => SourceSpan.From(Item, Comma);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Item, Comma);

	protected abstract ISourceElement Item { get; }
}
