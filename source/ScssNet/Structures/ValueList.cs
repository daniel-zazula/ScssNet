using System.Diagnostics;

namespace ScssNet.Structures;

public class ValueList : SourceElement, IValue
{
	public IReadOnlyList<IValue> Values { get; }

	public SourceCoordinates Start => Values.First().Start;
	public SourceCoordinates End => Values.Last().End;
	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Values);

	internal ValueList(IReadOnlyList<IValue> values)
	{
		Debug.Assert(values.Count > 1, "Value list must contain more than one value");
		Values = values;
	}
}
