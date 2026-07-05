namespace ScssNet.Structures;

public interface ISelector : ISyntaxStructure
{
}

/// <summary>
/// A composite selector is a selector that picks elements who satisfies multiple selectors at the same time.
/// </summary>
/// <remarks>For example, div.my-class or #some-id[some-attribute] are composite selectors.</remarks>
public interface ICompositeSelector : ISelector
{
	public ISelectorQualifier? Qualifier { get; }
}

/// <summary>
/// A complex selector is a selector that picks elements based on their relationship to other elements.
/// </summary>
/// <remarks>For example, .parent-class .descendant-class or .parent-class > .child-class are complex selectors.</remarks>
public interface IComplexSelector : ISelector
{
	ISelector Selector { get; }
}

/// <summary>
/// A qualifier is an selector that comes after the first selector of a compound selector.
/// </summary>
/// <remarks>For example, in the compound selector "div#my-id.my-class", "#my-id" and ".my-class" are qualifiers.</remarks>
public interface ISelectorQualifier : ICompositeSelector
{
}
