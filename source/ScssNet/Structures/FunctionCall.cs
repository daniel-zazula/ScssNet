using ScssNet.Tokens;

namespace ScssNet.Structures;

public class FunctionCall
(
	IdentifierToken name, SymbolToken openParenthesis, IValue? arguments, SymbolToken closeParenthesis
): ISyntaxStructure, IValue
{
	public IdentifierToken Name => name;

	public SymbolToken OpenParenthesis => openParenthesis;

	public IValue? Arguments => arguments;

	public SymbolToken CloseParenthesis => closeParenthesis;

	public SourceSpan Span => SourceSpan.From(name, closeParenthesis);

	public Issues Issues => Issues.ConcatFrom(name, openParenthesis, arguments, closeParenthesis);
}
