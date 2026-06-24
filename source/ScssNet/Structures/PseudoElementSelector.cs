using ScssNet.Tokens;

namespace ScssNet.Structures;

public class PseudoElementSelector
(
	SymbolToken doubleColon, IdentifierToken identifier, ISelectorQualifier? qualifier
) : SourceElement, ISelectorQualifier
{
	public SymbolToken DoubleColon => doubleColon;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(doubleColon, identifier, qualifier);

	public SourceCoordinates Start => doubleColon.Start;

	public SourceCoordinates End => LastEnd(identifier, qualifier);
}
