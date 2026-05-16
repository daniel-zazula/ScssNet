using ScssNet.Tokens;

namespace ScssNet.Structures;

public class Rule(IdentifierToken property, SymbolToken colon, IValue value, SymbolToken? semiColon) : ISyntaxStructure
{
	public IdentifierToken Property => property;
	public SymbolToken Colon => colon;
	public IValue Value => value;
	public SymbolToken? SemiColon => semiColon;

	public IEnumerable<Issue> Issues => SourceElement.List(Property, Colon, Value, SemiColon).ConcatIssues();

	public SourceCoordinates Start => Property.Start;

	public SourceCoordinates End => SemiColon?.End ?? Value.End;
}
