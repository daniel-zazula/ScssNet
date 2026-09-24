using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class HashValueTokenMatchingExtensions
{
	public static HashValueToken? MatchHashValue(this TokenReader tokenReader)
	{
		return tokenReader.Match(CheckHashValue);
	}

	private static HashValueToken? CheckHashValue(IToken? token)
	{
		return token is HashValueToken hashValueToken ? hashValueToken : default;
	}
}
