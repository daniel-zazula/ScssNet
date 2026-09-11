using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class MediaQueryTypeKeywordTokenCreation
{
	extension(MediaQueryTypeKeywordToken)
	{
		internal static MediaQueryTypeKeywordToken Create
		(
			MediaQueryTypeKeyword type, string? text = null, ISourceElement? predecessor = null
		)
		{
			text = MediaQueryTypeKeywordToken.CheckOrGetValue(type, text);
			var length = text.Length;
			var span = SourceSpan.Create(predecessor, length);

			return new MediaQueryTypeKeywordToken(type, text, span, Separator.Empty, Separator.Empty);
		}
	}
}
