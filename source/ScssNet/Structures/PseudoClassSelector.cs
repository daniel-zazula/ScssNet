using ScssNet.Tokens;

namespace ScssNet.Structures;

public class PseudoClassSelector
(
	SymbolToken colon, IdentifierToken identifier, ISelectorQualifier? qualifier
) : ISyntaxStructure, ISelectorQualifier
{
	public SymbolToken Colon => colon;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(colon, identifier, qualifier);

	public Issues Issues => Issues.ConcatFrom(colon, identifier, qualifier);
}
