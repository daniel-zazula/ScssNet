namespace ScssNet;

public interface ISourceElement
{
	SourceSpan Span { get; }
	Issues Issues { get; }
}

public interface IValue : ISourceElement { }
