using ScssNet.Tokens;

namespace ScssNet.Structures;

public interface IMediaQuery : ISyntaxStructure
{
}

public interface IMediaQueryExpression : ISyntaxStructure, IMediaQuery
{
}

public interface IMediaQueryValue : ISyntaxStructure, IMediaQuery, IMediaQueryExpression
{
}

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
