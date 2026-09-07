using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class MediaQueryOperatorKeywordTokenMatchingExtensions
{
	internal static MediaQueryOperatorKeywordToken? MatchKeyword<T>(this TokenReader tokenReader)
		where T : MediaQueryOperatorKeywordToken
	{
		var result = tokenReader.MatchKeyword<MediaQueryOperatorKeyword>();
		if(result is null)
			return null;

		return new MediaQueryOperatorKeywordToken(result.Value.keyword, result.Value.identifierToken);
	}
}
