using ScssNet.Tokens;

namespace ScssNet.Parsing;

internal static class TokenTrailingSeparatorExtensions
{
	public static bool HasTrailingSeparator(this ISeparatedToken token)
	{
		return token.TrailingSeparator != Separator.Empty;
	}

	public static bool HasTrailingSeparator(this IdentifierToken identifierToken)
	{
		return ((ISeparatedToken)identifierToken).HasTrailingSeparator();
	}
}
