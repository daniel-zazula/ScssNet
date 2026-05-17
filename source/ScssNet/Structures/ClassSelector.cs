using ScssNet.Tokens;

namespace ScssNet.Structures;

public class ClassSelector
(
	SymbolToken dot, IdentifierToken identifier, ISelectorQualifier? qualifier
) : SourceElement, ISelectorQualifier
{
	public SymbolToken Dot => dot;
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(dot, identifier, qualifier);

	public SourceCoordinates Start => dot.Start;

	public SourceCoordinates End => LastEnd(identifier, qualifier);
}
