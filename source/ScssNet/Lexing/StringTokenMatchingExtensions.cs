using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class StringTokenMatchingExtensions
{
	extension(TokenReader tokenReader)
	{
		public StringToken RequireString()
		{
			return tokenReader.Match<StringToken>()
				?? new StringToken(tokenReader.GetCoordinates(), Issue.CreateExpected("string"));
		}
	}
}
