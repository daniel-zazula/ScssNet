using ScssNet.Tokens;

namespace ScssNet.Structures;

public class AttributeSelector
(
	SymbolToken openBracket, IdentifierToken attribute, SymbolToken? @operator, StringToken? value,
	IdentifierToken? modifier, SymbolToken closeBracket, ISelectorQualifier? qualifier
) : SourceElement, ISyntaxStructure, ISelectorQualifier
{
	public SymbolToken OpenBracket => openBracket;
	public IdentifierToken Attribute => attribute;
	public SymbolToken? Operator => @operator;
	public StringToken? Value => value;
	public IdentifierToken? Modifier => modifier;
	public SymbolToken CloseBracket => closeBracket;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(openBracket, closeBracket);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(openBracket, attribute, @operator, value, modifier, closeBracket, qualifier);
}
