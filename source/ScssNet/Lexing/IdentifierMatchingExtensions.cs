using ScssNet.Tokens;

namespace ScssNet.Lexing;

internal static class IdentifierMatchingExtensions
{
	public static IdentifierToken? MatchIdentifier(this TokenReader tokenReader)
	{
		return tokenReader.Match(CheckToken);
	}
		
	public static IdentifierToken? MatchIdentifier(this TokenReader tokenReader, string? name)
	{
		return tokenReader.Match(t => t is IdentifierToken it && name.EqualsIgnoreCase(it.Text) ? it : default);
	}

	public static IdentifierToken RequireIdentifier(this TokenReader tokenReader)
	{
		return tokenReader.Match(CheckToken)
			?? new IdentifierToken(tokenReader.GetCoordinates(), Issue.CreateExpected("identifier"));
	}

	private static IdentifierToken? CheckToken(IToken? token)
	{
		return token is IdentifierToken identifierToken ? identifierToken : default;
	}
}
