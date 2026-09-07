using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class MediaQueryValueKeywordTokenMatchingExtensions
{
	internal static MediaQueryTypeKeywordToken? MatchKeyword<T>(this TokenReader tokenReader)
		where T : MediaQueryTypeKeywordToken
	{
		var result = tokenReader.MatchKeyword<MediaQueryTypeKeyword>();
		if(result is null)
			return null;

		return new MediaQueryTypeKeywordToken(result.Value.keyword, result.Value.identifierToken);
	}

	internal static MediaQueryTypeKeywordToken RequireKeyword<T>(this TokenReader tokenReader)
		where T : MediaQueryTypeKeywordToken
	{
		return tokenReader.MatchKeyword<T>() ?? MediaQueryTypeKeywordToken.CreateMissing(tokenReader.GetCoordinates());
	}
}
