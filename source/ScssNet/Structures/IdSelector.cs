using ScssNet.Tokens;

namespace ScssNet.Structures;

public class IdSelector
(
	HashValueToken id, ISelectorQualifier? qualifier
) : SourceElement, ISelectorQualifier
{
	public HashValueToken Id => id;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(id, Qualifier);

	public SourceCoordinates Start => Id.Start;

	public SourceCoordinates End => LastEnd(id, Qualifier);
}
