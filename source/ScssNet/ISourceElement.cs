namespace ScssNet;

public interface ISourceElement
{
	SourceSpan Span { get; }
	IEnumerable<Issue> Issues { get; }
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
}
