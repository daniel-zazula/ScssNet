using ScssNet.Tokens;

namespace ScssNet.Structures;

public class FunctionCall
(
	IdentifierToken name, SymbolToken openParenthesis, IValue? arguments, SymbolToken closeParenthesis
): SourceElement, ISyntaxStructure, IValue
{
	public IdentifierToken Name => name;

	public SymbolToken OpenParenthesis => openParenthesis;

	public IValue? Arguments => arguments;

	public SymbolToken CloseParenthesis => closeParenthesis;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(name, openParenthesis, arguments, closeParenthesis);

	public SourceCoordinates Start => name.Start;

	public SourceCoordinates End => closeParenthesis.End;
}
