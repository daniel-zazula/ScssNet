using System.Diagnostics;
using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ValueList : SourceElement, IValue
{
	public IReadOnlyList<ValueListItem> Items { get; }

	public SourceCoordinates Start => Items.First().Start;
	public SourceCoordinates End => Items.Last().End;
	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Items);

	internal ValueList(IReadOnlyList<ValueListItem> items)
	{
		Debug.Assert(items.Count > 1, "value list must contain more than one value");
		Items = items;
	}
}

public class ValueListItem(IValue value, SymbolToken? comma = null) : SourceElement, ISourceElement
{
	public IValue Value => value;

	public SymbolToken? Comma => comma;

	public SourceCoordinates Start => Value.Start;
	public SourceCoordinates End => LastEnd(Value, Comma);
	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Value, Comma);
}
