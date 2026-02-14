using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class TagSelector(IdentifierToken identifier, ISelectorQualifier? qualifier) : ICompositeSelector
{
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => SourceElement.List(identifier, qualifier).ConcatIssues();

	public SourceCoordinates Start => identifier.Start;

	public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();

	public bool HasSeparatorAfter()
	{
		return qualifier?.HasSeparatorAfter()
			?? Identifier.TrailingSeparator == Separator.Empty;
	}
}
