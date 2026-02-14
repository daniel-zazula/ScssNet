using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class IdSelector
(
	SymbolToken hash, IdentifierToken identifier, ISelectorQualifier? qualifier
) : ISelectorQualifier
{
	public SymbolToken Hash => hash;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => SourceElement.List(Hash, Identifier, Qualifier).ConcatIssues();

	public SourceCoordinates Start => Hash.Start;

	public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();

	public bool HasSeparatorAfter()
	{
		return qualifier?.HasSeparatorAfter()
			?? Identifier.TrailingSeparator == Separator.Empty;
	}
}
