using ScssNet.Tokens;

namespace ScssNet.Structures;

public class MediaQueryUnaryExpression
(
	MediaQueryOperatorKeywordToken operatorKeyword, MediaQueryTypeKeywordToken value
): ISyntaxStructure, IMediaQueryExpression
{
	public MediaQueryOperatorKeywordToken OperatorKeyword => operatorKeyword;

	public MediaQueryTypeKeywordToken Value => value;

	public SourceSpan Span => SourceSpan.From(operatorKeyword, value);

	public Issues Issues => Issues.ConcatFrom(operatorKeyword, value);
}
