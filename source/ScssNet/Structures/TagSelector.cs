using ScssNet.Tokens;

namespace ScssNet.Structures;

public class TagSelector(IdentifierToken identifier, ISelectorQualifier? qualifier)
	: SourceElement, ISyntaxStructure, ICompositeSelector
{
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Identifier, Qualifier);

	public SourceCoordinates Start => identifier.Start;

	public SourceCoordinates End => LastEnd(Identifier, Qualifier);
}
