namespace ScssNet.SourceElements;

public interface ISelector : ISourceElement
{
}

public interface ICompositeSelector : ISelector
{
	public ISelectorQualifier? Qualifier { get; }
}

public interface IComplexSelector : ISelector
{
	ISelector Selector { get; }
}

public interface ISelectorQualifier : ICompositeSelector
{
}
