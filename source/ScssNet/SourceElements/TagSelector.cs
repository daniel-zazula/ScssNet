using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class TagSelector(IdentifierToken identifier, ICompoundSelector? qualifier) : ISourceElement, ISelector
{
	public IdentifierToken Identifier => identifier;
	public ICompoundSelector? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => SourceElement.List(identifier, qualifier).ConcatIssues();

	public SourceCoordinates Start => identifier.Start;

	public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();
}
