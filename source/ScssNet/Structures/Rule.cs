using ScssNet.Tokens;

namespace ScssNet.Structures;

public class Rule(IdentifierToken property, SymbolToken colon, IValue value, SymbolToken? semiColon)
	: SourceElement, ISyntaxStructure
{
	public IdentifierToken Property => property;
	public SymbolToken Colon => colon;
	public IValue Value => value;
	public SymbolToken? SemiColon => semiColon;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Property, Colon, Value, SemiColon);

	public SourceCoordinates Start => Property.Start;

	public SourceCoordinates End => LastEnd(SemiColon, Value);
}
