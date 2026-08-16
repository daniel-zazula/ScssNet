using ScssNet.Tokens;

namespace ScssNet.Structures;

public abstract class ListItem : SourceElement, ISyntaxStructure, ISourceElement
{
	public SymbolToken? Comma { get; protected set; }

	public SourceCoordinates Start => Item.Start;
	public SourceCoordinates End => LastEnd(Item, Comma);
	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Item, Comma);

	protected abstract ISourceElement Item { get; }
}
