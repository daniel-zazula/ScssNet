using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class IdSelector(SymbolToken hash, IdentifierToken identifier, ICompoundSelector? qualifier) : ISourceElement, ISelector, ICompoundSelector
{
	public SymbolToken Hash => hash;
	public IdentifierToken Identifier => identifier;
	public ICompoundSelector? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => SourceElement.List(Hash, Identifier, Qualifier).ConcatIssues();

	public SourceCoordinates Start => Hash.Start;

	public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();
}
