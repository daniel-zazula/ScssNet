using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class IdentifierTokenCreation
{
	extension(IdentifierToken)
	{
		internal static IdentifierToken Create(string identifier, ISourceElement? predecessor = null)
		{
			var length = identifier.Length;
			var span = SourceSpan.Create(predecessor, length);

			return new IdentifierToken(identifier, span, Separator.Empty, Separator.Empty);
		}
	}
}
