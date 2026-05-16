namespace ScssNet.Structures;

public interface ISyntaxStructure : ISourceElement
{
}

public interface ISelector : ISyntaxStructure
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
