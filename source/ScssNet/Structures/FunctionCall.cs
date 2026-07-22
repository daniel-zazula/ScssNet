using ScssNet.Tokens;

namespace ScssNet.Structures;

public class FunctionCall
(
	IdentifierToken name, SymbolToken openParenthesis, ValueList? arguments, SymbolToken closeParenthesis
): SourceElement, IValue
{
	public IdentifierToken Name => name;

	public SymbolToken OpenParenthesis => openParenthesis;

	public ValueList? Arguments => arguments;

	public SymbolToken CloseParenthesis => closeParenthesis;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(name, openParenthesis, arguments, closeParenthesis);

	public SourceCoordinates Start => name.Start;

	public SourceCoordinates End => closeParenthesis.End;
}
