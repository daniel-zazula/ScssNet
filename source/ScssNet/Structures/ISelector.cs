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

/// <summary>
/// A qualifier is an selector that comes after the first selector of a compound selector.
/// For example, in the compound selector "div#my-id.my-class", "#my-id" and ".my-class" are qualifiers.
/// </summary>
public interface ISelectorQualifier : ICompositeSelector
{
}
