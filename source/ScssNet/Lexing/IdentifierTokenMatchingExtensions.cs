using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class IdentifierTokenMatchingExtensions
{
	extension(TokenReader tokenReader)
	{
		public IdentifierToken RequireIdentifier()
		{
			return tokenReader.Match<IdentifierToken>()
				?? new IdentifierToken(tokenReader.GetCoordinates(), Issue.CreateExpected("identifier"));
		}
	}
}
