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
			var operatorToken = MediaQueryOperatorKeywordToken.Create(operatorKeyword, operatorText, predecessor);
			var typeToken = MediaQueryTypeKeywordToken.Create(typeKeyword, typeText, operatorToken);

			return new MediaQueryUnaryExpression(operatorToken, typeToken);
		}
	}
}
