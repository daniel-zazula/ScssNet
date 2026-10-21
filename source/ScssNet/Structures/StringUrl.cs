using ScssNet.Tokens;

namespace ScssNet.Structures;

public class StringUrl
(
	StringToken value
) : ISyntaxStructure, IUrl
{
	public StringToken Value => value;
	public SourceSpan Span => value.Span;
	public Issues Issues => value.Issues;
}
