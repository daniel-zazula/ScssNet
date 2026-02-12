namespace ScssNet.SourceElements;

public interface ISelector : ISourceElement
{
	public ISelectorQualifier? Qualifier { get; }

	public bool HasSeparatorAfter();
}
