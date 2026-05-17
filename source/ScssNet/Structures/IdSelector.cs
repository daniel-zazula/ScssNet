using ScssNet.Tokens;

namespace ScssNet.Structures;

public class IdSelector
(
	SymbolToken hash, IdentifierToken identifier, ISelectorQualifier? qualifier
) : SourceElement, ISelectorQualifier
{
	public SymbolToken Hash => hash;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Hash, Identifier, Qualifier);

	public SourceCoordinates Start => Hash.Start;

	public SourceCoordinates End => LastEnd(Identifier, Qualifier);
}
