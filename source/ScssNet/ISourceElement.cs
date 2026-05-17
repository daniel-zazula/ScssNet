namespace ScssNet;

public interface ISourceElement
{
	IEnumerable<Issue> Issues { get; }
	SourceCoordinates Start { get; }
	SourceCoordinates End { get; }
}

public interface IValue : ISourceElement { }

public abstract class SourceElement
{
	protected static IEnumerable<Issue> ConcatIssuesFrom(params ISourceElement?[] sourceElements)
	{
		return ConcatIssuesFrom((IEnumerable<ISourceElement?>)sourceElements);
	}

	protected static IEnumerable<Issue> ConcatIssuesFrom(IEnumerable<ISourceElement?> sourceElements)
	{
		return sourceElements.SelectMany(se => se?.Issues ?? []);
	}

	protected static SourceCoordinates LastEnd(params ISourceElement?[] sourceElements)
	{
		return sourceElements.Last(se => se is not null)!.End;
	}
}
