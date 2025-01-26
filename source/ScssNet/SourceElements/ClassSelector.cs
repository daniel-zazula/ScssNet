using ScssNet.Tokens;

namespace ScssNet.SourceElements;

public class ClassSelector(SymbolToken dot, IdentifierToken identifier, ICompoundSelector? qualifier) : ISourceElement, ISelector, ICompoundSelector
{
	public SymbolToken Dot => dot;
	public IdentifierToken Identifier => identifier;
	public ICompoundSelector? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => SourceElement.List(dot, identifier, qualifier).ConcatIssues();

	public SourceCoordinates Start => dot.Start;

	public SourceCoordinates End => SourceElement.List(identifier, qualifier).LastEnd();
}
