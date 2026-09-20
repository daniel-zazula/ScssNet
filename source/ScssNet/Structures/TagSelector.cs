using ScssNet.Tokens;

namespace ScssNet.Structures;

public class TagSelector(IdentifierToken identifier, ISelectorQualifier? qualifier)
	: ISyntaxStructure, ICompositeSelector
{
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(identifier, qualifier);

	public Issues Issues => Issues.ConcatFrom(Identifier, Qualifier);
}
