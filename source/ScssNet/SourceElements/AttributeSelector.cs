using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class AttributeSelector
(
	SymbolToken openBracket, IdentifierToken attribute, SymbolToken? @operator, StringToken? value,
	IdentifierToken? modifier, SymbolToken closeBracket, ISelectorQualifier? qualifier
) : ISourceElement, ISelector, ISelectorQualifier
{
	public SymbolToken OpenBracket => openBracket;
	public IdentifierToken Attribute => attribute;
	public SymbolToken? Operator => @operator;
	public StringToken? Value => value;
	public IdentifierToken? Modifier => modifier;
	public SymbolToken CloseBracket => closeBracket;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => SourceElement.List(openBracket, attribute, @operator, value, modifier, closeBracket, qualifier).ConcatIssues();

	public SourceCoordinates Start => openBracket.Start;

	public SourceCoordinates End => SourceElement.List(closeBracket, qualifier).LastEnd();

	public bool HasSeparatorAfter()
	{
		return qualifier?.HasSeparatorAfter()
			?? CloseBracket.TrailingSeparator == Separator.Empty;
	}
}
