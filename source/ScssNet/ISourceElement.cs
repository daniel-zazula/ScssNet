namespace ScssNet
{
	public interface ISourceElement
	{
		ICollection<Issue> Issues { get; }
		SourceCoordinates Start { get; }
		SourceCoordinates End { get; }
	}
}
