using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class ValueKeywordTokenMatchingExtensions
{
	internal static ValueKeywordToken? MatchKeyword<T>(this TokenReader tokenReader) where T : ValueKeywordToken
	{
		var result = tokenReader.MatchKeyword<ValueKeyword>();
		if(result is null)
			return null;

		return new ValueKeywordToken(result.Value.keyword, result.Value.identifierToken);
	}

	internal static ValueKeywordToken RequireKeyword<T>(this TokenReader tokenReader) where T : ValueKeywordToken
	{
		return tokenReader.MatchKeyword<T>() ?? ValueKeywordToken.CreateMissing(tokenReader.GetCoordinates());
	}
}
