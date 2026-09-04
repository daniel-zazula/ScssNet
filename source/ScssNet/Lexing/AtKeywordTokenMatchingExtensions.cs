using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class AtKeywordTokenMatchingExtensions
{
	internal static AtKeywordToken? MatchKeyword<T>(this TokenReader tokenReader) where T: AtKeywordToken
	{
		var result = tokenReader.MatchKeyword<AtKeyword>();
		if (result is null)
			return null;

		return new AtKeywordToken(result.Value.keyword, result.Value.identifierToken);
	}

	internal static AtKeywordToken RequireKeyword<T>(this TokenReader tokenReader) where T : AtKeywordToken
	{
		return tokenReader.MatchKeyword<T>() ?? AtKeywordToken.CreateMissing(tokenReader.GetCoordinates());
	}
}
