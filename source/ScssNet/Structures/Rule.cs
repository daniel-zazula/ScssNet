using ScssNet.Tokens;

namespace ScssNet.Structures;

public class Rule
(
	IdentifierToken property, SymbolToken colon, IValue value, ImportantValue? important, SymbolToken? semiColon
) : SourceElement, ISyntaxStructure
{
	public IdentifierToken Property => property;
	public SymbolToken Colon => colon;
	public IValue Value => value;
	public SymbolToken? SemiColon => semiColon;
	public ImportantValue? Important => important;

	public SourceSpan Span => SourceSpan.From(property, value, semiColon, important);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Property, Colon, Value, Important, SemiColon);
}
