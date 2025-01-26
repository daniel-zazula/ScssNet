namespace ScssNet.SourceElements
{
	public interface IValue : ISourceElement { }

	public class MissingValue : IValue
	{
		public SourceCoordinates Start { get; }
		public SourceCoordinates End { get; }
		public IEnumerable<Issue> Issues { get; }

		internal MissingValue(SourceCoordinates start)
		{
			Start = start;
			End = start;
			Issues = [new Issue(IssueType.Error, "Missing value (measure unit, string or function)")];
		}
	}
}
