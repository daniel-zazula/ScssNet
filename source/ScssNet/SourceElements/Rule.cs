using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class Rule(IdentifierToken property, SymbolToken colon, IValue value, SymbolToken semiColon) : ISourceElement
{
	public IdentifierToken Property => property;
	public SymbolToken Colon => colon;
	public IValue Value => value;
	public SymbolToken SemiColon => semiColon;

	public IEnumerable<Issue> Issues => SourceElement.List(Property, Colon, Value, SemiColon).ConcatIssues();

	public SourceCoordinates Start => Property.Start;

	public SourceCoordinates End => SemiColon.End;
}
