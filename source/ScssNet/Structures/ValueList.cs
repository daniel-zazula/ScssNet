using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ValueList : ISyntaxStructure, IValue
{
	public IReadOnlyList<ValueListItem> Items { get; }

	public SourceSpan Span => SourceSpan.From(Items.Cast<ISourceElement?>());
	public Issues Issues => Issues.ConcatFrom(Items);

	public ValueList(ICollection<ValueListItem> items)
	{
		if(items.Count == 0)
			throw new ArgumentException("Items cannot be empty", nameof(items));

		Items = Array.AsReadOnly(items.ToArray());
	}
}

public class ValueListItem : ListItem, ISyntaxStructure, ISourceElement
{
	public IValue Value { get; }

	protected override ISourceElement Item => Value;

	public ValueListItem(IValue value, SymbolToken? comma = null)
	{
		Value = value;
		Comma = comma;
	}
}
