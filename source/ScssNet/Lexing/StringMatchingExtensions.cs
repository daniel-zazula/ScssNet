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
			?? new MissingStringToken(tokenReader.GetCoordinates());
	}

	private static StringToken? CheckString(IToken? token)
	{
		return token is StringToken stringToken ? stringToken : default;
	}
}
