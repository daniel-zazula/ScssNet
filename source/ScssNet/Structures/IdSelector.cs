using ScssNet.Tokens;

namespace ScssNet.Structures;

public class IdSelector
(
	HashValueToken identifier, ISelectorQualifier? qualifier
) : SourceElement, ISyntaxStructure, ISelectorQualifier
{
	public HashValueToken Identifier => identifier;
	public ISelectorQualifier? Qualifier => qualifier;

	public SourceSpan Span => SourceSpan.From(identifier, qualifier);

	public IEnumerable<Issue> Issues => ConcatIssuesFrom(identifier, Qualifier);
}
