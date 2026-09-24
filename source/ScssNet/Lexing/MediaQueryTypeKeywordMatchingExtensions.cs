using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class MediaQueryTypeKeywordMatchingExtensions
{
	internal static MediaQueryTypeKeywordToken? MatchMediaQueryTypeKeyword(this TokenReader tokenReader)
	{
		var identifierToken = tokenReader.MatchKeyword<MediaQueryTypeKeyword>(out var keyword);
		if (identifierToken is null)
			return null;

		return new MediaQueryTypeKeywordToken(keyword, identifierToken);
	}

	internal static MediaQueryTypeKeywordToken RequireMediaQueryTypeKeyword(this TokenReader tokenReader)
	{
		return tokenReader.MatchMediaQueryTypeKeyword()
			?? new MediaQueryTypeKeywordToken(tokenReader.GetCoordinates(), Issue.CreateExpectedOneOf<MediaQueryTypeKeyword>());
	}
}
