using ScssNet.Tokens;

namespace ScssNet.Test.ElementCreation;

internal static class SymbolTokenCreation
{
	extension(SymbolToken)
	{
		internal static SymbolToken Create(Symbol symbol, ISourceElement? predecessor = null)
		{
			var length = symbol.ToChars().Length;
			var span = SourceSpan.Create(predecessor, length);

			return new SymbolToken(symbol, span, Separator.Empty, Separator.Empty);
		}
	}
}
