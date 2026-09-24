using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class StringMatchingExtensions
{
	public static StringToken? MatchString(this TokenReader tokenReader)
	{
		return tokenReader.Match(CheckString);
	}

	public static StringToken RequireString(this TokenReader tokenReader)
	{
		return tokenReader.Match(CheckString)
			?? new StringToken(tokenReader.GetCoordinates(), Issue.CreateExpected("string"));
	}

	private static StringToken? CheckString(IToken? token)
	{
		return token is StringToken stringToken ? stringToken : default;
	}
}
