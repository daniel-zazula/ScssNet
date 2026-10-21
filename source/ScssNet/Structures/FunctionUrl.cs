namespace ScssNet.Structures;

public class FunctionUrl
(
	FunctionCall value
) : ISyntaxStructure, IUrl
{
	public FunctionCall Value => value;
	public SourceSpan Span => value.Span;
	public Issues Issues => value.Issues;
}
