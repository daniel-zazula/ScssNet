using ScssNet.Tokens;

namespace ScssNet.Structures;

public class IdSelector
(
	HashValueToken identifier, ISelectorQualifier? qualifier
) : ISyntaxStructure, ISelectorQualifier
{
	public HashValueToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(identifier, qualifier);

	public Issues Issues => Issues.ConcatFrom(identifier, Qualifier);
}
