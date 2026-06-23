using ScssNet.Tokens;

namespace ScssNet.Structures;

public class IdSelector
(
	HashValueToken identifier, ISelectorQualifier? qualifier
) : SourceElement, ISelectorQualifier
{
	public HashValueToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(identifier, Qualifier);

	public SourceCoordinates Start => Identifier.Start;

	public SourceCoordinates End => LastEnd(identifier, Qualifier);
}
