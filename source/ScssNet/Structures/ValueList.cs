using System.Diagnostics;

namespace ScssNet.Structures;

public record ValueList : IValue
{
	public IReadOnlyList<IValue> Values { get; }

	public SourceCoordinates Start => Values.First().Start;
	public SourceCoordinates End => Values.Last().End;
	public IEnumerable<Issue> Issues => Values.ConcatIssues();

	internal ValueList(IReadOnlyList<IValue> values)
	{
		Debug.Assert(values.Count > 0, "Value list must contain at least one value");
		Values = values;
	}
}
