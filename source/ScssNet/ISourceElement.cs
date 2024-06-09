namespace ScssNet
{
	public interface ISourceElement
	{
		IEnumerable<Issue> Issues { get; }
		SourceCoordinates Start { get; }
		SourceCoordinates End { get; }
	}

	internal static class SourceElement
	{
		internal static IEnumerable<ISourceElement?> List(params ISourceElement?[] sourceElements)
		{
			return sourceElements;
		}

		internal static IEnumerable<Issue> ConcatIssues(this IEnumerable<ISourceElement?> sourceElements)
		{
			return sourceElements.SelectMany(se => se?.Issues ?? []);
		}

		internal static SourceCoordinates LastEnd(this IEnumerable<ISourceElement?> sourceElements)
		{
			return sourceElements.Last(se => se is not null)!.End;
		}
	}
}
