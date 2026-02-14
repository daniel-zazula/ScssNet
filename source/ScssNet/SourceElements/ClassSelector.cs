using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class ClassSelector
(
	SymbolToken dot, IdentifierToken identifier, ISelectorQualifier? qualifier
) : ISelectorQualifier
{
	public SymbolToken Dot => dot;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => SourceElement.List(dot, identifier, qualifier).ConcatIssues();

	public SourceCoordinates Start => dot.Start;

	public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();

	public bool HasSeparatorAfter()
	{
		return qualifier?.HasSeparatorAfter()
			?? Identifier.TrailingSeparator == Separator.Empty;
	}
}
