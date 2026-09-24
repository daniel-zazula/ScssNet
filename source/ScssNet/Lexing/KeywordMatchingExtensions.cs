using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class KeywordMatchingExtensions
{
	internal static KeywordToken<T>? MatchKeyword<T>(this TokenReader tokenReader) where T : struct, Enum
	{
		T keyword = default;
		var identifierToken = tokenReader.Match(t => CheckToken(t, out keyword));
		if (identifierToken is null)
			return null;

		return new KeywordToken<T>(keyword, identifierToken);
	}

	internal static IdentifierToken? MatchKeyword<T>(this TokenReader tokenReader, out T keyword) where T : struct, Enum
	{
		T keywordMatch = default;
		var token = tokenReader.Match(t => CheckToken(t, out keywordMatch));
		keyword = keywordMatch;
		return token;
	}

	internal static KeywordToken<T> RequireKeyword<T>(this TokenReader tokenReader) where T : struct, Enum
	{
		return tokenReader.MatchKeyword<T>()
			?? new KeywordToken<T>(tokenReader.GetCoordinates(), Issue.CreateExpectedOneOf<T>());
	}

	private static IdentifierToken? CheckToken<T>(IToken? token, out T keywordMatch) where T : struct, Enum
	{
		if (token is IdentifierToken identifierToken)
		{
			var text = identifierToken.Text;
			var keywords = (T[])Enum.GetValues(typeof(T));
			foreach(var keyword in keywords)
			{
				if(text.EqualsIgnoreCase(keyword.ToString()))
				{
					keywordMatch = keyword;
					return identifierToken;
				}
			}
		}

		keywordMatch = default;
		return null;
	}
}
