using ScssNet.Tokens;

namespace ScssNet.Structures;

public abstract class ListItem : ISyntaxStructure
{
	public SymbolToken? Comma { get; protected set; }

	public SourceSpan Span => SourceSpan.From(Item, Comma);

	public Issues Issues => Issues.ConcatFrom(Item, Comma);

	protected abstract ISourceElement Item { get; }
}
