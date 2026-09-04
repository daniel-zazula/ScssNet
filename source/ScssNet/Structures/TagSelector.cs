using ScssNet.Tokens;

namespace ScssNet.Structures;

public class TagSelector(IdentifierToken identifier, ISelectorQualifier? qualifier)
	: SourceElement, ISyntaxStructure, ICompositeSelector
{
	public IdentifierToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(identifier, qualifier);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(Identifier, Qualifier);
}
