using ScssNet.Tokens;

namespace ScssNet.Structures;

public class PseudoElementSelector
(
	SymbolToken doubleColon, IdentifierToken identifier, ISelectorQualifier? qualifier
) : ISyntaxStructure, ISelectorQualifier
{
	public SymbolToken DoubleColon => doubleColon;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(doubleColon, identifier, qualifier);

	public Issues Issues => Issues.ConcatFrom(doubleColon, identifier, qualifier);
}
