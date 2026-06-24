using ScssNet.Tokens;

namespace ScssNet.Structures;

public class PseudoClassSelector
(
	SymbolToken colon, IdentifierToken identifier, ISelectorQualifier? qualifier
) : SourceElement, ISelectorQualifier
{
	public SymbolToken Colon => colon;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(colon, identifier, qualifier);

	public SourceCoordinates Start => colon.Start;

	public SourceCoordinates End => LastEnd(identifier, qualifier);
}
