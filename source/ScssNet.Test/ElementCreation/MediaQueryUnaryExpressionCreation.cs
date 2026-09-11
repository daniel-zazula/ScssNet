using ScssNet.Structures;
using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class MediaQueryUnaryExpressionCreation
{
	extension(MediaQueryUnaryExpression)
	{
		internal static MediaQueryUnaryExpression Create
		(
			MediaQueryOperatorKeyword operatorKeyword, MediaQueryTypeKeyword typeKeyword,
			string? operatorText = null, string? typeText = null,
			ISourceElement? predecessor = null
		)
		{
			var operatorToken = CreateMediaQueryOperator(operatorKeyword, operatorText, predecessor);
			var typeToken = MediaQueryTypeKeywordToken.Create(typeKeyword, typeText, operatorToken);

			return new MediaQueryUnaryExpression(operatorToken, typeToken);
		}

		private static MediaQueryOperatorKeywordToken CreateMediaQueryOperator
		(
			MediaQueryOperatorKeyword operatorKeyword, string? text = null, ISourceElement? predecessor = null
		)
		{
			text = MediaQueryOperatorKeywordToken.CheckOrGetValue(operatorKeyword, text);
			var length = text.Length;
			var span = SourceSpan.Create(predecessor, length);

			return new MediaQueryOperatorKeywordToken(operatorKeyword, text, span, Separator.Empty, Separator.Empty);
		}
	}
}
