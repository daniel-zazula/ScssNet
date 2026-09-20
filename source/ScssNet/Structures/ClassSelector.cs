using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ClassSelector
(
	SymbolToken dot, IdentifierToken identifier, ISelectorQualifier? qualifier
) : ISyntaxStructure, ISelectorQualifier
{
	public SymbolToken Dot => dot;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(dot, identifier, qualifier);

	public Issues Issues => Issues.ConcatFrom(dot, identifier, qualifier);
}
