namespace ScssNet.SourceElements;

public interface ISelector : ISourceElement
{
	public bool HasSeparatorAfter();
}

public interface ICompositeSelector : ISelector
{
	public ISelectorQualifier? Qualifier { get; }
}

public interface ISelectorQualifier : ICompositeSelector
{
}
